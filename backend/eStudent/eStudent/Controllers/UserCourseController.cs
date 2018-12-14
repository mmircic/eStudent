using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.Request;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserCourseController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserCourseController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IEnumerable<UserCourse> GetRequests()
        {
            return _context.UserCourses;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var request = await _context.UserCourses.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequest(int id, [FromBody] RequestUpdateDto request)
        {
            UserCourse entity = _mapper.Map<RequestUpdateDto, UserCourse>(request);
            entity.Id = id;

            _context.UserCourses.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] RequestCreateDto request)
        {

            UserCourse entity = _mapper.Map<RequestCreateDto, UserCourse>(request);
            entity.Accepted = false;

            _context.UserCourses.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new
            {
                id = entity.Id
            }, entity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.UserCourses.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.UserCourses.Remove(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        private bool RequestExists(int id)
        {
            return _context.UserCourses.Any(e => e.Id == id);
        }
    }
}