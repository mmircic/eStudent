using eStudent.DTO;
using eStudent.DTO.CourseType;
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
    public class CourseTypesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CourseTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/CourseTypes
        [HttpGet]
        public IEnumerable<CourseType> GetCourseTypes()
        {
            return _context.CourseTypes;
        }

        // GET: api/CourseTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseType(int id)
        {
            var courseType = await _context.CourseTypes.FindAsync(id);

            if (courseType == null)
            {
                return NotFound();
            }

            return Ok(courseType);
        }

        // PUT: api/CourseTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseType(int id, [FromBody] CourseTypeUpdateDto courseType)
        {
            CourseType entity = await _context.CourseTypes.FindAsync(id);

            if (entity == null)
            {
                return BadRequest();
            }

            entity.Type = courseType.Type;

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        // POST: api/CourseTypes
        [HttpPost]
        public async Task<IActionResult> PostCourseType([FromBody] CourseTypeCreateDto courseType)
        {
            CourseType entity = new CourseType()
            {
                Type = courseType.Type
            };
            _context.CourseTypes.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseType", new { id = entity.Id }, entity);
        }

        // DELETE: api/CourseTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseType(int id)
        {
            var courseType = await _context.CourseTypes.FindAsync(id);
            if (courseType == null)
            {
                return NotFound();
            }

            _context.CourseTypes.Remove(courseType);
            await _context.SaveChangesAsync();

            return Ok(courseType);
        }

        private bool CourseTypeExists(int id)
        {
            return _context.CourseTypes.Any(e => e.Id == id);
        }
    }
}