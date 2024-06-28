using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonModels;
using BookingApi.InterfaceServices;
using System.Collections.Generic;
using System.Security.Claims;

namespace BookingAPI.Controllers
{
    [Authorize(Policy = "ViewBooking")]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Obtenir toutes les réservations (basé sur le rôle)
        [HttpGet]
        [Authorize(Policy = "ViewBooking")]
        public ActionResult<IEnumerable<BookingModel>> GetAllBookings()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role == "Patient")
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var patientBookings = _bookingService.GetBookingsForPatient(userId);
                return Ok(patientBookings);
            }
            else // Admin ou Practitioner
            {
                var allBookings = _bookingService.GetAllBookings();
                return Ok(allBookings);
            }
        }

        // Obtenir une réservation par ID (basé sur le rôle)
        [HttpGet("{id}")]
        [Authorize(Policy = "ViewBooking")]
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

        // Créer une nouvelle réservation (pour les Patients)
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult<BookingModel> CreateBooking([FromBody] BookingModel booking)
        {
            _bookingService.CreateBooking(booking);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }

        // Mettre à jour une réservation (uniquement pour les Admins)
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

        // Supprimer une réservation (uniquement pour les Admins)
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteBooking(int id)
        {
            _bookingService.DeleteBooking(id);
            return NoContent();
        }
    }
}