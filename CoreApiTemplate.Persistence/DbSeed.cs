using CoreApiTemplate.Domain.Identities;
using CoreApiTemplate.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiTemplate.Persistence
{
    public class DbSeed
    {
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = (ApplicationContext)scope.ServiceProvider.GetService(typeof(ApplicationContext));

                context.Database.EnsureCreated();

                context.Database.Migrate();

                await SeedRoleAsync(context);

                await SeedUserAsync(scope, context);
            }
        }

        private static async Task SeedRoleAsync(ApplicationContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new ApplicationRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

                context.Roles.Add(new ApplicationRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                });

                await context.SaveChangesAsync(default);
            }
        }

        private static async Task SeedUserAsync(IServiceScope scope, ApplicationContext context)
        {
            if (context.Users.SingleOrDefault(x => x.UserName.Equals("admin@success-ss.com.vn")) == null)
            {
                var userManager = (UserManager<ApplicationUser>)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));

                var admin = new ApplicationUser
                {
                    UserName = "admin@success-ss.com.vn",
                    Email = "admin@success-ss.com.vn",
                    NormalizedEmail = "ADMIN@SUCCESS-SS.COM.VN",
                    NormalizedUserName = "ADMIN@SUCCESS-SS.COM.VN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await userManager.CreateAsync(admin, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "ADMIN");
                }

                var normalUser = new ApplicationUser
                {
                    UserName = "testuser1@yopmail.com",
                    NormalizedUserName = "testuser1@yopmail.com",
                    Email = "testuser1@yopmail.com",
                    NormalizedEmail = "testuser1@yopmail.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                result = await userManager.CreateAsync(normalUser, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "USER");
                }
            }
        }
    }
}
