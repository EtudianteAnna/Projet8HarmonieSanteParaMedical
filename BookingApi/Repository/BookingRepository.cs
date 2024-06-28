using CommonModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BookingApi.DbContext;
using BookingApi.InterfaceRepository;

namespace BookingApi.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDb _context;

        public BookingRepository(BookingDb context)
        {
            _context = context;
        }

        public IEnumerable<BookingModel?> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public BookingModel? GetBookingById(int id)
        {
            return _context.Bookings.Find(id);
        }

        public void CreateBooking(BookingModel booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void UpdateBooking(BookingModel? booking)
        {
            _context.Bookings.Update(booking);
            _context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
        }
    }
}