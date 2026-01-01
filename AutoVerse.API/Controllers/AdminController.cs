using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AutoVerse.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> rolemanager, UserManager<ApplicationUser> usermanager)
        {
            _roleManager = rolemanager;
            _userManager = usermanager;
        }


        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Role already exists.");
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                return StatusCode(500, result.Errors);
            }
            Log.Information($"New Role created: {roleName}");
            return Ok($"Role {roleName} created successfully.");
        }

        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);
            Log.Warning($"Role deleted: {roleName}");
            return NoContent();
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(string userEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return BadRequest("User not found.");
            }
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return BadRequest("Role not found.");
            }
            await _userManager.AddToRoleAsync(user, roleName);
            Log.Information($"Assigning {roleName} to user: {user}");
            return Ok($"Assigned {roleName} to user: {user}");
        }
    }
}
