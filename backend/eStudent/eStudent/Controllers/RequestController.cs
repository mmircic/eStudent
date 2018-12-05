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
    public class RequestController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public RequestController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IEnumerable<Request> GetRequests()
        {
            return _context.Requests;
        }


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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, [FromBody] RequestUpdateDto request)
        {
            Request entity = _mapper.Map<RequestUpdateDto, Request>(request);
            entity.Id = id;

            _context.Requests.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] RequestCreateDto request)
        {

            Request entity = _mapper.Map<RequestCreateDto, Request>(request);
            entity.Accepted = false;

            _context.Requests.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new
            {
                id = entity.Id
            }, entity);
        }


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