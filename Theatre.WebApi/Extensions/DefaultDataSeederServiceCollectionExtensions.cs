using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Theatre.Data.Core.Models;
using Theatre.Data.Core.Services;

namespace Theatre.WebApi.Extensions
{
    public static class DefaultDataSeederServiceCollectionExtensions
    {
        public static IServiceCollection CreateDefaultRolesAndUsers(this IServiceCollection services)
        {
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<ApplicationUser>>();

            var adminRole = roleManager.FindByNameAsync("Administrator").GetAwaiter().GetResult();
            if (adminRole == null)
            {
                var createAdminRoleResult = roleManager.CreateAsync(new IdentityRole() { Name = "Administrator", NormalizedName = "ADMINISTRATOR" }).GetAwaiter().GetResult();
            }

            var userRole = roleManager.FindByNameAsync("User").GetAwaiter().GetResult();
            if (userRole == null)
            {
                var createUserRoleResult = roleManager.CreateAsync(new IdentityRole() { Name = "User", NormalizedName = "USER" }).GetAwaiter().GetResult();
            }

            var adminUser = userManager.FindByEmailAsync("admin@test.com").GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new ApplicationUser() { Email = "admin@test.com", UserName = "admin@test.com" };

                userManager.CreateAsync(adminUser, "123456_Aa").GetAwaiter().GetResult();

                var hasAdminRole = userManager.GetRolesAsync(adminUser).GetAwaiter().GetResult().Any(r => r == "Administrator");
                if (!hasAdminRole)
                {
                    userManager.AddToRoleAsync(adminUser, "Administrator").GetAwaiter().GetResult();
                }

            }

            var user = userManager.FindByNameAsync("user@test.com").GetAwaiter().GetResult();
            if (user == null)
            {
                user = new ApplicationUser() { Email = "user@test.com", UserName = "user@test.com" };

                userManager.CreateAsync(user, "123456_Aa").GetAwaiter().GetResult();

                var hasUserRole = userManager.GetRolesAsync(user).GetAwaiter().GetResult().Any(r => r == "User");
                if (!hasUserRole)
                {
                    userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
                }
            }

            var spectacleService = services.BuildServiceProvider().GetRequiredService<ISpectacleService>();
            var count = spectacleService.GetAll().Count();
            if (count == 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    spectacleService.Create(new Spectacle() { Title = $"Spectacle #{i}" });
                }
            }

            return services;
        }
    }
}
