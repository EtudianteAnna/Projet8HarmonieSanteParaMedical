using CommonModels;
using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Praticiens.Services;

namespace PraticiensApi.Controllers
{
    [Authorize(Policy = "ViewPraticien")]
    [ApiController]
    [Route("api/[controller]")]
    public class PraticienCalendarController : ControllerBase
    {
        private readonly IPraticienCalendarRepository _calendarService;

        public PraticienCalendarController(IPraticienCalendarRepository calendarService)
        {
            _calendarService = calendarService;
        }

        // La politique d'autorisation "ManageCalendar" est définie et gérée au niveau de l'API Gateway(Ocelot)
        [HttpGet]
        [Authorize(Policy = "ManageCalendar")] // Policy définie dans CommonModels
        public ActionResult<IEnumerable<PraticienCalendars>> GetAllCalendars()
        {
            return Ok(_calendarService.GetAllCalendars());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ManageCalendar")]
        public ActionResult<PraticienCalendars> GetCalendarById(int id)
        {
            var calendar = _calendarService.GetCalendarById(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return Ok(calendar);
        }

        [HttpPost]
        [Authorize(Policy = "ManageCalendar")]
        public IActionResult CreateCalendar([FromBody] PraticienCalendars calendar)
        {
            if (calendar == null)
            {
                return BadRequest("Le corps de la requête ne peut pas être null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _calendarService.CreateCalendar(calendar);
                return StatusCode(StatusCodes.Status201Created, calendar);
            }
            catch (Exception)
            {
                // Vous pouvez logger l'exception ici
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur est survenue lors de la création du calendrier.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ManageCalendar")]
        public IActionResult DeleteCalendar(int id)
        {
            _calendarService.DeleteCalendar(id);
            return NoContent();
        }
    }
}