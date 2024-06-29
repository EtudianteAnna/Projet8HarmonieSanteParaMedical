using Microsoft.AspNetCore.Mvc;

namespace HSPMFront.Controller
{
    [Route("praticien")]
    public class PraticienController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("http://localhost:7160/swagger/index.html");
        }
    }
}
