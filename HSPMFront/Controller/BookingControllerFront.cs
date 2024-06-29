using Microsoft.AspNetCore.Mvc;

namespace HSPMFront.Controller
{
    [Route("booking")]
    public class BookingController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("http://localhost:7081/swagger/index.html");
        }
    }
}

