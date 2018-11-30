using eStudent.DTO;
using eStudent.DTO.Subject;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SubjectsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public IEnumerable<Subject> GetSubjects()
        {
            return _context.Subjects;
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, [FromBody] SubjectUpdateDto subject)
        {
            Subject entity = await _context.Subjects.FindAsync(id);
            if (entity == null)
            {
                return BadRequest();
            }

            entity.Name = subject.Name;
            entity.ECTSPoints = subject.ECTSPoints.Value;
            entity.YearOfStudy = subject.YearOfStudy.Value;
            entity.CourseId = subject.CourseId.Value;

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<IActionResult> PostSubject([FromBody] SubjectCreateDto subject)
        {
            Subject entity = new Subject()
            {
                Name = subject.Name,
                ECTSPoints = subject.ECTSPoints.Value,
                YearOfStudy = subject.YearOfStudy.Value,
                CourseId = subject.CourseId.Value
            };
            _context.Subjects.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = entity.Id }, entity);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok(subject);
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}