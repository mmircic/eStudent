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
    [ApiController]
    public class StudentController : ControllerBase
    {
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
            var users = _context.Users.Include(u => u.Role).Where(u => u.Role.Name == "Student").ToList();
            var students = this._mapper.Map<IEnumerable<User>, IEnumerable<StudentGetDto>>(users);

            return students;
        }
    }
}
