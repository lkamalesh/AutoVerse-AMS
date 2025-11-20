using AutoVerse.Core.DTOs;
using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> usermanager, IConfiguration configuration)
        {
            _userManager = usermanager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {
                UserName = register.Email,
                Email = register.Email,
                FullName = register.FullName
            };

            var result = await _userManager.CreateAsync(user, register.ConfirmPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _userManager.AddToRoleAsync(user, "Customer");
            Log.Information($"New user registered: {user.Email}");
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                Log.Warning($"Failed login attempt for: {login.Email}");
                return Unauthorized("Invalid username or password");
            }
            var userRole = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken((ApplicationUser)user, userRole);
            Log.Information($"User logged in: {user.Email}");
            return Ok(new { Token = token });

        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.FullName!),
            };
            
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
     
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddMinutes(30)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
