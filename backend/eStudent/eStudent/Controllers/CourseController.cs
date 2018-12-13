using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.Course;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CourseController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IEnumerable<Course> GetCourses()
        {
            var courses = _context.Courses.ToList();
            return courses;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseUpdateDto course)
        {


            Course entity = _mapper.Map<CourseUpdateDto, Course>(course);
            entity.Id = id;


            _context.Courses.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseCreateDto course)
        {
            Course entity = _mapper.Map<CourseCreateDto, Course>(course);

            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = entity.Id }, entity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}