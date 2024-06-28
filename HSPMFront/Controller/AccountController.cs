using CommonModels;
using AuthService.Login;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace HSPMFront.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuth _authService;

        public AccountController(IAuth authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpGet("login")]
        public IActionResult Login() => View();

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model.Username, model.Password);
                if (result)
                {
                    var token = await _authService.GenerateTokenAsync(model.Username);
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                    if (jwtToken != null)
                    {
                        var roles = jwtToken.Claims
                            .Where(claim => claim.Type == ClaimTypes.Role)
                            .Select(claim => claim.Value)
                            .ToList();

                        if (roles.Contains("admin"))
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (roles.Contains("patient"))
                        {
                            return RedirectToAction("Index", "Booking");
                        }
                        else if (roles.Contains("praticien"))
                        {
                            return RedirectToAction("Index", "Praticien");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpGet("register")]
        public IActionResult Register() => View();

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    Roles = new List<string> { model.Role } // Assurez-vous que UserRegister a une propriété Role
                };

                var result = await _authService.RegisterAsync(user);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "User registration failed.");
            }

            return View(model);
        }

        // Autres actions pour ForgotPassword et ResetPassword
    }
}
