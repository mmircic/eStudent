using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.CourseType;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseTypeController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public CourseTypeController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IEnumerable<CourseType> GetCourseTypes()
        {
            return _context.CourseTypes;
        }


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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseType(int id, [FromBody] CourseTypeUpdateDto courseType)
        {
            CourseType entity = _mapper.Map<CourseTypeUpdateDto, CourseType>(courseType);
            entity.Id = id;

            _context.CourseTypes.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> PostCourseType([FromBody] CourseTypeCreateDto courseType)
        {
            CourseType entity = _mapper.Map<CourseTypeCreateDto, CourseType>(courseType);

            _context.CourseTypes.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseType", new { id = entity.Id }, entity);
        }


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