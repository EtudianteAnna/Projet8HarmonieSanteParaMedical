
using AuthService.Login;
using CommonModels.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace AuthService.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<AppUser>> _mockUserManager;
        private readonly Mock<SignInManager<AppUser>> _mockSignInManager;
        private readonly Services.AuthService _authService;

       /* public AuthServiceTests()
        {
            _mockUserManager = new Mock<UserManager<AppUser>>(
                new Mock<IUserStore<AppUser>>().Object, null, null, null, null, null, null, null, null
            );
            _mockSignInManager = new Mock<SignInManager<AppUser>>(
                _mockUserManager.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<AppUser>>().Object,
                null, null, null, null
            );
            _authService = _authService(_mockUserManager.Object, _mockSignInManager.Object);
        }*/

        [Fact]
        public async Task LoginAsync_ReturnsTrue_WhenCredentialsAreValid()
        {
            // Arrange
            _mockSignInManager.Setup(s => s.PasswordSignInAsync("validUser", "validPassword", false, false))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _authService.LoginAsync("validUser", "validPassword");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsFalse_WhenCredentialsAreInvalid()
        {
            // Arrange
            _mockSignInManager.Setup(s => s.PasswordSignInAsync("invalidUser", "invalidPassword", false, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act
            var result = await _authService.LoginAsync("invalidUser", "invalidPassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GenerateTokenAsync_ReturnsToken()
        {
            // Act
            var result = await _authService.GenerateTokenAsync("validUser");

            // Assert
            Assert.Equal("generated_token", result);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsTrue_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var userModel = new User { Username = "newUser", Password = "Password123!", Email = "newUser@example.com" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), userModel.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.RegisterAsync(userModel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsFalse_WhenRegistrationFails()
        {
            // Arrange
            var userModel = new User { Username = "newUser", Password = "Password123!", Email = "newUser@example.com" };
            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), userModel.Password))
                .ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _authService.RegisterAsync(userModel);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ForgotPasswordAsync_ReturnsTrue_WhenUserExists()
        {
            // Arrange
            var user = new AppUser { UserName = "existingUser", Email = "existingUser@example.com" };
            _mockUserManager.Setup(u => u.FindByEmailAsync("existingUser@example.com")).ReturnsAsync(user);

            // Act
            var result = await _authService.ForgotPasswordAsync("existingUser@example.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ForgotPasswordAsync_ReturnsFalse_WhenUserDoesNotExist()
        {
            // Arrange
            _mockUserManager.Setup(u => u.FindByEmailAsync("nonExistentUser@example.com")).ReturnsAsync((AppUser)null);

            // Act
            var result = await _authService.ForgotPasswordAsync("nonExistentUser@example.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ResetPasswordAsync_ReturnsTrue_WhenResetIsSuccessful()
        {
            // Arrange
            var user = new AppUser { UserName = "existingUser", Email = "existingUser@example.com" };
            _mockUserManager.Setup(u => u.FindByEmailAsync("existingUser@example.com")).ReturnsAsync(user);
            _mockUserManager.Setup(u => u.ResetPasswordAsync(user, "validToken", "NewPassword123!"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _authService.ResetPasswordAsync("existingUser@example.com", "validToken", "NewPassword123!");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ResetPasswordAsync_ReturnsFalse_WhenResetFails()
        {
            // Arrange
            var user = new AppUser { UserName = "existingUser", Email = "existingUser@example.com" };
            _mockUserManager.Setup(u => u.FindByEmailAsync("existingUser@example.com")).ReturnsAsync(user);
            _mockUserManager.Setup(u => u.ResetPasswordAsync(user, "invalidToken", "NewPassword123!"))
                .ReturnsAsync(IdentityResult.Failed());

            // Act
            var result = await _authService.ResetPasswordAsync("existingUser@example.com", "invalidToken", "NewPassword123!");

            // Assert
            Assert.False(result);
        }
    }
}

