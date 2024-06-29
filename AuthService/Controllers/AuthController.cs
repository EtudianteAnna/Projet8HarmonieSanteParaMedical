using Microsoft.AspNetCore.Mvc;
using CommonModels;
using AuthService.Login;
using CommonModels.Login;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            var result = await _authService.LoginAsync(model.Username, model.Password);
            if (!result)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            var token = await _authService.GenerateTokenAsync(model.Username);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var result = await _authService.RegisterAsync(model);
            if (!result)
            {
                return BadRequest(new { Message = "User registration failed" });
            }
            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
        {
            var result = await _authService.ForgotPasswordAsync(model.Email);
            if (!result)
            {
                return NotFound(new { Message = "User not found" });
            }
            return Ok(new { Message = "Password reset token sent" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            var result = await _authService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
            if (!result)
            {
                return BadRequest(new { Message = "Password reset failed" });
            }
            return Ok(new { Message = "Password reset successfully" });
        }
    }
}