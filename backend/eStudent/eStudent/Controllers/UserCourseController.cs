using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using eStudent.DTO;
using eStudent.DTO.Request;
using eStudent.Models;
using eStudent.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        private IConverter _converter;

        public UserCourseController(DatabaseContext context, IMapper mapper, IConverter converter)
        {
            _context = context;
            _mapper = mapper;
            _converter = converter;
        }


        [HttpGet("all")]
        public IEnumerable<UserCourse> GetUserCourse()
        {
            return _context.UserCourses;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCourse(int id)
        {
            var request = await _context.UserCourses.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        [HttpGet("pdf/{id}")]
        public IActionResult GetUserCoursePdf(int id)
        {
            var userCourse = _context.UserCourses
                .Include(uc => uc.User)
                .Include(uc => uc.Course)
                .ThenInclude(c => c.SubjectCourses)
                .ThenInclude(sc => sc.Subject)
                .FirstOrDefault(p => p.Id == id);

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Izvješće o upisu"
            };

            var objectSettings = new ObjectSettings
            {
                //PagesCount = true,
                HtmlContent = TemplateGenerator.GetHtmlString(userCourse)
                //WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };


            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "EmployeeReport.pdf");

            //if (userCourse == null)
            //{
            //    return NotFound();
            //}

            //return Ok(userCourse);
        }

        [HttpGet("unaccepted/all")]
        public IEnumerable<UserCourseGetDto> GetAllUnacceptedUserCourse()
        {
            List<UserCourse> requests = _context.UserCourses
                .Include(uc => uc.User)
                .Include(uc => uc.Course)
                .Where(uc => !uc.Accepted).ToList();
            List<UserCourseGetDto> requestsList = _mapper.Map<List<UserCourse>, List<UserCourseGetDto>>(requests);
            return requestsList;
        }

        [HttpGet("accepted/all")]
        public IEnumerable<UserCourseGetDto> GetAllAcceptedUserCourse()
        {
            List<UserCourse> requests = _context.UserCourses
                .Include(uc => uc.User)
                .Include(uc => uc.Course)
                .Where(uc => uc.Accepted).ToList();
            List<UserCourseGetDto> requestsList = _mapper.Map<List<UserCourse>, List<UserCourseGetDto>>(requests);
            return requestsList;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserCourse(int id, [FromBody] UserCourseUpdateDto userCourse)
        {
            UserCourse entity = await _context.UserCourses.FindAsync(id);
            entity.Accepted = userCourse.Accepted;

            _context.UserCourses.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserCourse([FromBody] UserCourseCreateDto userCourse)
        {

            UserCourse entity = _mapper.Map<UserCourseCreateDto, UserCourse>(userCourse);
            entity.Accepted = false;
            entity.Date = DateTime.UtcNow;

            _context.UserCourses.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new
            {
                id = entity.Id
            }, entity);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCourse(int id)
        {
            var result = await _context.UserCourses.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.UserCourses.Remove(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        private bool UserCourseExists(int id)
        {
            return _context.UserCourses.Any(e => e.Id == id);
        }
    }
}