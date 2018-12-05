using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.User;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("all")]
        public IEnumerable<User> GetUsers()
        {
            var users = _context.Users;
            return users;
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UserUpdateDto user)
        {

            User entity = _mapper.Map<UserUpdateDto, User>(user);
            entity.Id = id;

            _context.Users.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserCreateDto user)
        {
            User entity = _mapper.Map<UserCreateDto, User>(user);
            entity.RoleId = 6;

            _context.Users.Add(entity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = entity.Id }, entity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}