namespace AutoOglasi.Data.Seeding
{
    using AutoOglasi.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;
    using static GlobalConstants.GlobalConstants;

    public class AdministratorSeeder : ISeeder
    {
        private const string AdminEmailKey = "SeedAdmin:Email";
        private const string AdminPasswordKey = "SeedAdmin:Password";

        public async Task SeedAsync(AutoOglasiDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            await SeedAdminAsync(roleManager, userManager, configuration, AdministratorRoleName);
        }

        private static async Task SeedAdminAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            string adminRoleName)
        {
            var adminEmail = configuration[AdminEmailKey];
            var adminPassword = configuration[AdminPasswordKey];

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new InvalidOperationException("Set SeedAdmin:Email and SeedAdmin:Password in user secrets before starting the application.");
            }

            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                var adminRole = new IdentityRole { Name = adminRoleName };
                await roleManager.CreateAsync(adminRole);
            }

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true,
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to seed the administrator account: {string.Join("; ", createResult.Errors)}");
                }
            }
            else
            {
                adminUser.Email = adminEmail;
                adminUser.UserName = adminEmail;
                adminUser.EmailConfirmed = true;

                var updateResult = await userManager.UpdateAsync(adminUser);
                if (!updateResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to update the administrator account: {string.Join("; ", updateResult.Errors)}");
                }

                if (await userManager.HasPasswordAsync(adminUser))
                {
                    var removePasswordResult = await userManager.RemovePasswordAsync(adminUser);
                    if (!removePasswordResult.Succeeded)
                    {
                        throw new InvalidOperationException($"Unable to reset the administrator password: {string.Join("; ", removePasswordResult.Errors)}");
                    }
                }

                var addPasswordResult = await userManager.AddPasswordAsync(adminUser, adminPassword);
                if (!addPasswordResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to set the administrator password: {string.Join("; ", addPasswordResult.Errors)}");
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
            {
                var roleResult = await userManager.AddToRoleAsync(adminUser, adminRoleName);
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to assign the administrator role: {string.Join("; ", roleResult.Errors)}");
                }
            }
        }
    }
}
