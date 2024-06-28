using System;
using System.Threading.Tasks;
using ApiGateway.Controllers;
using AuthService.Login;
using CommonModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiGateway.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuth> _mockAuthService;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockAuthService = new Mock<IAuth>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Login_ReturnsOk_WithValidCredentials()
        {
            // Arrange
            var model = new UserLogin { Username = "validUsername", Password = "validPassword" };
            _mockAuthService.Setup(auth => auth.LoginAsync(model.Username, model.Password)).ReturnsAsync(true);
            _mockAuthService.Setup(auth => auth.GenerateTokenAsync(model.Username)).ReturnsAsync("validToken");

            // Act
            var result = await _controller.Login(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("validToken", returnValue.GetType().GetProperty("Token").GetValue(returnValue, null));
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WithInvalidCredentials()
        {
            // Arrange
            var model = new UserLogin { Username = "invalidUsername", Password = "invalidPassword" };
            _mockAuthService.Setup(auth => auth.LoginAsync(model.Username, model.Password)).ReturnsAsync(false);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.NotNull(unauthorizedResult);
            Assert.IsType<UnauthorizedObjectResult>(result);
            var returnValue = unauthorizedResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("Invalid credentials", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }

        [Fact]
        public async Task Register_ReturnsOk_WithValidUser()
        {
            // Arrange
            var model = new User { Username = "newUser", Password = "newPassword", Email = "newuser@example.com" };
            _mockAuthService.Setup(auth => auth.RegisterAsync(model)).ReturnsAsync(true);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("User registered successfully", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WithInvalidUser()
        {
            // Arrange
            var model = new User { Username = "", Password = "newPassword", Email = "newuser@example.com" };
            _mockAuthService.Setup(auth => auth.RegisterAsync(model)).ReturnsAsync(false);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = badRequestResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("User registration failed", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }

        [Fact]
        public async Task ForgotPassword_ReturnsOk_WhenEmailExists()
        {
            // Arrange
            var model = new ForgotPassword { Email = "existinguser@example.com" };
            _mockAuthService.Setup(auth => auth.ForgotPasswordAsync(model.Email)).ReturnsAsync(true);

            // Act
            var result = await _controller.ForgotPassword(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("Password reset token sent", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }

        [Fact]
        public async Task ForgotPassword_ReturnsNotFound_WhenEmailDoesNotExist()
        {
            // Arrange
            var model = new ForgotPassword { Email = "nonexistinguser@example.com" };
            _mockAuthService.Setup(auth => auth.ForgotPasswordAsync(model.Email)).ReturnsAsync(false);

            // Act
            var result = await _controller.ForgotPassword(model);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = notFoundResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("User not found", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }

        [Fact]
        public async Task ResetPassword_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var model = new ResetPassword { Email = "user@example.com", Token = "validToken", NewPassword = "newPassword" };
            _mockAuthService.Setup(auth => auth.ResetPasswordAsync(model.Email, model.Token, model.NewPassword)).ReturnsAsync(true);

            // Act
            var result = await _controller.ResetPassword(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("Password reset successfully", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }

        [Fact]
        public async Task ResetPassword_ReturnsBadRequest_WhenUnsuccessful()
        {
            // Arrange
            var model = new ResetPassword { Email = "user@example.com", Token = "invalidToken", NewPassword = "newPassword" };
            _mockAuthService.Setup(auth => auth.ResetPasswordAsync(model.Email, model.Token, model.NewPassword)).ReturnsAsync(false);

            // Act
            var result = await _controller.ResetPassword(model);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.IsType<BadRequestObjectResult>(result);
            var returnValue = badRequestResult.Value;
            Assert.NotNull(returnValue);
            Assert.Equal("Password reset failed", returnValue.GetType().GetProperty("Message").GetValue(returnValue, null));
        }
    }
}
