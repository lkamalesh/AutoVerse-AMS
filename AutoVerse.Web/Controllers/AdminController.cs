using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AutoVerse.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> rolemanager, UserManager<ApplicationUser> usermanager)
        {
            _roleManager = rolemanager;
            _userManager = usermanager;  
        }


        [HttpGet,HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return Content($"Invalid Role Name {roleName}!");
            }
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return Content("Role already exists!");
    
            }
            Log.Information($"New Role created: {roleName}");
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return Content($"Role {roleName} created successfully.");
        }

        [HttpGet,HttpPost]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return Content("Role not found.");
            }

            Log.Warning($"Role deleted: {roleName}");
            await _roleManager.DeleteAsync(role);
            return Content($"Role {roleName} deleted successfully.");
        }

        [HttpGet,HttpPost]
        public async Task<IActionResult> AssignRole(string userEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return Content("User not found.");
            }
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return Content("Role not found.");
            }
            Log.Information($"Assigning {roleName} to user: {user}");
            await _userManager.AddToRoleAsync(user, roleName);
            return Ok($"Assigned role {roleName} to user {userEmail}.");
        }
                

    }
}
