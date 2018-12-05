using eStudent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace eStudent.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private DatabaseContext _context;

        public AuthController(IConfiguration config, DatabaseContext context)
        {
            _config = config;
            _context = context;
        }
        [HttpPost]
        public IActionResult Auth([FromBody] Auth auth)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(auth);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private object GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("OIB", user.OIB),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("BirthDate", user.BirthDate.ToString()),
                new Claim("Residence", user.Residence),
                new Claim("Email", user.Email),
                new Claim("Role", user.Role.Name)

            };

            var token = new JwtSecurityToken(
                //issuer
                _config["Jwt:Issuer"],
                //audience
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User AuthenticateUser(Auth auth)
        {
            User user = null;

            user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == auth.Email && u.Password == auth.Password);

            return user;
        }
    }
}
