using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonModels;
using BookingApi.InterfaceServices;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookingAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Authorize(Policy = "BookingPolicy")]
        public ActionResult<IEnumerable<BookingModel>> GetAllBookings()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Patient")
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var patientBookings = _bookingService.GetBookingsForPatient(userId);
                return Ok(patientBookings);
            }
            else
            {
                var allBookings = _bookingService.GetAllBookings();
                return Ok(allBookings);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "BookingPolicy")]
        public ActionResult<BookingModel> GetBookingById(int id)
        {
            var booking = _bookingService.GetBookingById(id);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (role == "Patient" && booking.UserId != userId)
            {
                return Forbid();
            }

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult<BookingModel> CreateBooking([FromBody] BookingModel booking)
        {
            _bookingService.CreateBooking(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult UpdateBooking(int id, [FromBody] BookingModel booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }
            _bookingService.UpdateBooking(booking);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteBooking(int id)
        {
            _bookingService.DeleteBooking(id);
            return NoContent();
        }
    }
}
