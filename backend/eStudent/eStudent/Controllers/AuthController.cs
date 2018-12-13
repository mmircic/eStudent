using eStudent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IConfiguration config, DatabaseContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> AuthAsync([FromBody] Auth auth)
        {
            IActionResult response = Unauthorized();
            //var user = AuthenticateUser(auth);
            var singInResult = await _signInManager.PasswordSignInAsync(auth.Email, auth.Password, isPersistent: true, lockoutOnFailure: false);

            if (singInResult.Succeeded)
            {
                var user = await _userManager.Users.Include(p => p.Roles).ThenInclude(p => p.Role).FirstOrDefaultAsync(p => p.Email == auth.Email);
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
                new Claim("id", user.Id.ToString()),
                new Claim("oib", user.OIB),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("birthDate", user.BirthDate.ToString()),
                new Claim("residence", user.Residence),
                new Claim("email", user.Email),
                new Claim("role", user.Roles.First().Role.Name)

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

            user = _context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Email == auth.Email && u.PasswordHash == auth.Password);

            return user;
        }
    }
}
