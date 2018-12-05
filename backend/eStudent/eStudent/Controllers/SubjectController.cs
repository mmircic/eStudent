using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.Subject;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public SubjectController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IEnumerable<Subject> GetSubjects()
        {
            return _context.Subjects;
        }


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


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, [FromBody] SubjectUpdateDto subject)
        {
            Subject entity = _mapper.Map<SubjectUpdateDto, Subject>(subject);
            entity.Id = id;

            _context.Subjects.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> PostSubject([FromBody] SubjectCreateDto subject)
        {
            Subject entity = _mapper.Map<SubjectCreateDto, Subject>(subject);

            _context.Subjects.Add(entity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = entity.Id }, entity);
        }


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