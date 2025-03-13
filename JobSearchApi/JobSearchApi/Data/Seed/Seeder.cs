using JobSearchApi.Models;
using Microsoft.AspNetCore.Identity;

namespace JobSearchApi.Data.Seed
{
    public class Seeder
    {
        public async Task Seed(ILogger<Seeder> logger,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
        {
            if (roleManager.Roles.Count() == 0)
            {
                logger.LogCritical("There is no roles");
                var adminRole = new IdentityRole("Admin");
                var userRole = new IdentityRole("User");
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);
            }

            var admin = await userManager.FindByNameAsync("Admin");
            if (admin is null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "Admin@admin.com"
                };
                await userManager.CreateAsync(newAdmin, "Admin_123");
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
            roleManager.Roles.ToList().ForEach(x => logger.LogCritical($"Role ID: {x.Id}\t {x.Name}\n"));

        }
    }
}
