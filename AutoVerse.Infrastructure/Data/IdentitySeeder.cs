using AutoVerse.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVerse.Infrastructure.Data
{
    public static class IdentitySeeder
    {
        // Seed Initial Admin roles ->
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "administrator@autoverse.com";
            string adminPass = "iamAdmin@123";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {

                var admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Administrator"
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
