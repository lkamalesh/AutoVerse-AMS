using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace AutoVerse.Infrastructure.Data
{
    public static class IdentitySeeder
    {
        // Seed Initial Admin roles ->
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            var roles = new[] { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var jwtSettings = config.GetSection("AdminData");
            string adminEmail = jwtSettings["Email"]!;
            string adminPass = jwtSettings["Password"]!;

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {

                var admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Administrator",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPass);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
