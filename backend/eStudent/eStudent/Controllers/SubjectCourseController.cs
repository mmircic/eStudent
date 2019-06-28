using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.SujectCourse;
using eStudent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubjectCourseController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public SubjectCourseController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/SubjectCourses
        [HttpGet]
        public IEnumerable<SubjectCourse> GetSubjectCourses()
        {
            return _context.SubjectCourses;
        }

        // GET: api/SubjectCourses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectCourse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjectCourse = await _context.SubjectCourses.FindAsync(id);

            if (subjectCourse == null)
            {
                return NotFound();
            }

            return Ok(subjectCourse);
        }

        [HttpGet("course/{id}")]
        public IActionResult GetCourseSubjects([FromRoute] int id)
        {
            //var subjectCourse = await _context.SubjectCourses.FindAsync(id);

            var subjectCourse = _context.SubjectCourses
                .Include(sc => sc.Subject)
                .Where(sc => sc.Course.Id == id)
                .ToList();


            if (!subjectCourse.Any())
            {
                return NotFound();
            }

            //var subjectsLessCode = subjectCourse.Select(sc => sc.Subject);
            List<Subject> subjects = new List<Subject>();
            subjectCourse.ForEach(sc => subjects.Add(sc.Subject));

            List<SubjectGetDto> subjectsDto = _mapper.Map<List<Subject>, List<SubjectGetDto>>(subjects);

            return Ok(subjectsDto);
        }

        // PUT: api/SubjectCourses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjectCourse([FromRoute] int id, [FromBody] SubjectCourse subjectCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subjectCourse.SubjectId)
            {
                return BadRequest();
            }

            _context.Entry(subjectCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubjectCourses
        [HttpPost]
        public async Task<IActionResult> PostSubjectCourse([FromBody] SubjectCourseCreateDto subjectCourse)
        {
            var entity = _mapper.Map<SubjectCourseCreateDto, SubjectCourse>(subjectCourse);
            _context.SubjectCourses.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubjectCourseExists(subjectCourse.SubjectId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubjectCourse", new { id = subjectCourse.SubjectId }, subjectCourse);
        }


        [HttpDelete("{subjectId}/{courseId}")]
        public async Task<IActionResult> DeleteSubjectCourse([FromRoute] int subjectId, [FromRoute] int courseId)
        {
            var subjectCourse = _context.SubjectCourses.Where(sc => sc.SubjectId == subjectId && sc.CourseId == courseId).FirstOrDefault();
            if (subjectCourse == null)
            {
                return NotFound();
            }

            _context.SubjectCourses.Remove(subjectCourse);
            await _context.SaveChangesAsync();

            return Ok(subjectCourse);
        }

        private bool SubjectCourseExists(int id)
        {
            return _context.SubjectCourses.Any(e => e.SubjectId == id);
        }
    }
}