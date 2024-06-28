using System.Collections.Generic;
using System.Linq;
using CommonModels;
using BookingApi.InterfaceServices;

namespace BookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly List<BookingModel> _bookings;
        private int _nextId;

        public BookingService()
        {
            _bookings = new List<BookingModel>();
            _nextId = 1;
        }

        public IEnumerable<BookingModel> GetAllBookings()
        {
            return _bookings;
        }

        public BookingModel GetBookingById(int id)
        {
            return _bookings.FirstOrDefault(b => b.Id == id);
        }

        public BookingModel CreateBooking(BookingModel booking)
        {
            booking.Id = _nextId++;
            _bookings.Add(booking);
            return booking;
        }

        public bool UpdateBooking(BookingModel updatedBooking)
        {
            var existingBooking = _bookings.FirstOrDefault(b => b.Id == updatedBooking.Id);
            if (existingBooking == null)
            {
                return false;
            }

            existingBooking.Patient = updatedBooking.Patient;
            existingBooking.Consultant = updatedBooking.Consultant;
            existingBooking.Facility = updatedBooking.Facility;
            existingBooking.Payment = updatedBooking.Payment;
            existingBooking.Appointment = updatedBooking.Appointment;

            return true;
        }

        public bool DeleteBooking(int id)
        {
            var booking = _bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return false;
            }

            _bookings.Remove(booking);
            return true;
        }

        public object GetBookingsForPatient(string? userId)
        {
            throw new NotImplementedException();
        }
    }
}