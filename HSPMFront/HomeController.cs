using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace HSPMFront.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("redirect")]
        public IActionResult GetRedirectBasedOnRole()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (token != null)
            {
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

                }
            }
            // Si aucune condition n'est remplie, redirigez vers une vue par défaut
            return RedirectToAction("Index", "Home");
        }

    }
}

                