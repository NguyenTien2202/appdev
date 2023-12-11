using AspnetIdentityRoleBasedTutorial.Constants;
using AspnetIdentityRoleBasedTutorial.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace AspnetIdentityRoleBasedTutorial.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            // Seed Roles
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRolesAsync(roleManager);
            await SeedAdminAsync(userManager);
            await SeedStaffAsync(userManager);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Staff.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Staff.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Roles.User.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }
        }

        private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminEmail = "admin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Tiencute",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }

        private static async Task SeedStaffAsync(UserManager<ApplicationUser> userManager)
        {
            var staffEmail = "staff@gmail.com";

            var staffUser = await userManager.FindByEmailAsync(staffEmail);

            if (staffUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = staffEmail,
                    Email = staffEmail,
                    Name = "Tiencute",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                await userManager.CreateAsync(user, "Staff@123");
                await userManager.AddToRoleAsync(user, Roles.Staff.ToString());
            }
        }
    }
}
