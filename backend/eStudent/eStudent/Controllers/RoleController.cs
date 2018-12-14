using AutoMapper;
using eStudent.DTO;
using eStudent.DTO.Role;
using eStudent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(DatabaseContext context, IMapper mapper, RoleManager<Role> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _roleManager = roleManager;
        }


        [HttpGet("all")]
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleUpdateDto role)
        {
            Role entity = _mapper.Map<RoleUpdateDto, Role>(role);
            entity.Id = id;

            await _roleManager.UpdateAsync(entity);

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateDto role)
        {
            Role entity = _mapper.Map<RoleCreateDto, Role>(role);

            await _roleManager.CreateAsync(entity);

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}