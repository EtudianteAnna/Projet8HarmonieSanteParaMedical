using Microsoft.AspNetCore.Mvc;

namespace HSPMFront.Controllers
{
    [Route("booking")]
    public class BookingController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("http://localhost:7081/swagger/index.html");
        }
    }
}

