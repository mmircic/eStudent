using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.User;
using eStudent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    //[ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public UserManager<User> _userManager { get; set; }

        public UserController(DatabaseContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto user)
        {

            User entity = _context.Users.Find(id);
            entity.OIB = user.OIB;
            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.BirthDate = user.BirthDate.Value;
            entity.Residence = user.Residence;
            entity.Email = user.Email;
            entity.UserName = user.Email;

            try
            {
                var updatedUser = await _userManager.UpdateAsync(entity);
                if (updatedUser.Succeeded)
                {
                    return Ok(entity);
                }
                return BadRequest(updatedUser.Errors);
            }
            catch (DbUpdateException ex)
            {
                var innerEx = ex.InnerException as PostgresException;
                if (innerEx != null && innerEx.SqlState == "23505")
                {
                    return BadRequest(new[] { new { Code = "DuplicateOIB", Description = string.Empty } });
                }
                return BadRequest(ex.Message);
            }


        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto user)
        {
            User entity = _mapper.Map<UserCreateDto, User>(user);
            entity.UserName = user.Email;

            try
            {
                var userFromDb = await _userManager.CreateAsync(entity, entity.PasswordHash);
                if (!userFromDb.Succeeded)
                {
                    return BadRequest(userFromDb.Errors);
                }
                var roleResult = await _userManager.AddToRoleAsync(entity, "STUDENT");

                return Ok();
            }
            catch (DbUpdateException ex)
            {
                var innerEx = ex.InnerException as PostgresException;
                if (innerEx != null && innerEx.SqlState == "23505")
                {
                    return BadRequest(new[] { new { Code = "DuplicateOIB", Description = string.Empty } });
                }
                return BadRequest(ex.Message);
            }



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