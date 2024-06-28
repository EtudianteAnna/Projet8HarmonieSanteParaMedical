using CommonModels;

namespace BookingApi.InterfaceRepository
{
    public interface IBookingRepository
    {
        IEnumerable<BookingModel?> GetAllBookings();
        BookingModel? GetBookingById(int id);
        void CreateBooking(BookingModel? booking);
        void UpdateBooking(BookingModel? booking);
        void DeleteBooking(int id);
    }
}