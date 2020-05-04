using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Theatre.Data.Core.Models;
using Theatre.Dtos;

namespace Theatre.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("SignUp")]
        //POST : /api/Account/SignUp
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, dto.Password);
                await _userManager.AddToRoleAsync(applicationUser, "User");

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        //POST : /api/Account/Authenticate
        public async Task<IActionResult> Authenticate(AuthenticateDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email),
                        new Claim("role", roles.First()),
                    }),
                    Expires = DateTime.Now.AddHours(Convert.ToInt32(_configuration["ApplicationSettings:JWT_ExpireHours"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Wrong username or password." });
            }
        }
    }
}