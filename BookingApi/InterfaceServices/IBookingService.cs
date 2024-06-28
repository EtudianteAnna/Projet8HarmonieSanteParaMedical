using CommonModels;


namespace BookingApi.InterfaceServices
{
    public interface IBookingService
    {
        IEnumerable<BookingModel?> GetAllBookings();
        BookingModel GetBookingById(int id);
        BookingModel? CreateBooking(BookingModel booking);
        bool UpdateBooking(BookingModel? booking);
       bool DeleteBooking(int id);
        object GetBookingsForPatient(string? userId);

    }
}