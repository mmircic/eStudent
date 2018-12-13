using AutoMapper;
using eStudent.DTO.User;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    //[ApiController]
    public class StudentController : ControllerBase
    {
        private const string STUDENT_ROLE = "Student";
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public StudentController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IEnumerable<StudentGetDto> GetStudents()
        {
            Role studentRole = _context.Roles.Where(r => r.Name == STUDENT_ROLE).FirstOrDefault();
            //var users = _context.Users.Include(u => u.Roles).Where(u => u.Roles.Contains()).ToList();
            var users = _context.Users.Include(u => u.Roles).Where(u => u.Roles.Select(r => r.RoleId).Contains(studentRole.Id)).ToList();
            var students = this._mapper.Map<IEnumerable<User>, IEnumerable<StudentGetDto>>(users);

            return students;
        }
    }
}
