using Microsoft.AspNetCore.Mvc;

namespace HSPMFront.Controllers
{
    [Route("praticien")]
    public class PraticienController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("http://localhost:7160/swagger/index.html");
        }
    }
}
