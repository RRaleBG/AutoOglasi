using System.Threading.Tasks;
using AutoOglasi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AutoOglasi.Services.Tests
{
    public class LoginTests
    {
        [Fact]
        public async Task Login_WithValidCredentials_Succeeds()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "admin@admin.rs",
                UserName = "admin@admin.rs",
                NormalizedEmail = "ADMIN@ADMIN.RS",
                NormalizedUserName = "ADMIN@ADMIN.RS",
                EmailConfirmed = true
            };
            var userManager = TestIdentityManagers.CreateUserManager(new[] { user });
            var password = "Admin123";
            await userManager.AddPasswordAsync(user, password);

            var signInManager = new TestSignInManager(userManager);

            // Act
            var result = await signInManager.PasswordSignInAsync(
                user.Email,
                password,
                isPersistent: false,
                lockoutOnFailure: false);

            // Assert
            Assert.True(result.Succeeded);
        }
    }

    // Minimal test double for SignInManager
    public class TestSignInManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public TestSignInManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return SignInResult.Failed;
            if (await _userManager.CheckPasswordAsync(user, password))
                return SignInResult.Success;
            return SignInResult.Failed;
        }
    }
}
