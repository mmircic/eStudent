using eStudent.DTO;
using eStudent.DTO.Request;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RequestsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public IEnumerable<Request> GetRequests()
        {
            return _context.Requests;
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, [FromBody] RequestUpdateDto request)
        {
            Request entity = await _context.Requests.FindAsync(id);

            if (entity == null)
            {
                return BadRequest();
            }

            entity.CourseId = request.CourseId.Value;
            entity.YearOfStudy = request.YearOfStudy.Value;

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] RequestCreateDto request)
        {
            Request entity = new Request()
            {

                Accepted = false,
                CourseId = request.CourseId.Value,
                Date = DateTime.Now,
                //Authenticated user
                UserId = request.UserId.Value,
                YearOfStudy = request.YearOfStudy.Value

            };


            _context.Requests.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new
            {
                id = entity.Id
            }, entity);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}