using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HSPMFront.Controllers
{
    public class BookingController : Controller
    {
        [Authorize(Policy = "ViewBooking")]
        public IActionResult Index()
        {
            return View();
        }
    }

    public class PraticienController : Controller
    {
        [Authorize(Policy = "ViewPraticien")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
 