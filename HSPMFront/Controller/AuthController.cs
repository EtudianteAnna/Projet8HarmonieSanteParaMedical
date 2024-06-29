using CommonModels;
using CommonModels.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HSPMFront.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public AuthController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/auth/api/auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                return Ok(new { Token = token });
            }
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO model) 
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/auth/api/auth/register", model);
            if (response.IsSuccessStatusCode)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            return BadRequest(new { Message = "User registration failed" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword model)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/auth/api/auth/forgot-password", model);
            if (response.IsSuccessStatusCode)
            {
                return Ok(new { Message = "Password reset token sent" });
            }
            return NotFound(new { Message = "User not found" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/auth/api/auth/reset-password", model);
            if (response.IsSuccessStatusCode)
            {
                return Ok(new { Message = "Password reset successfully" });
            }
            return BadRequest(new { Message = "Password reset failed" });
        }
    }
}

