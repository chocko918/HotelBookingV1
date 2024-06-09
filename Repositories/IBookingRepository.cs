
using HotelBooking2.Models;

namespace HotelBooking2.Repositories
{
    public interface IBookingRepository
    {
        Task ConfirmBooking();

        Task<List<BookingRoom>> GetAllBookingRoomItems();

        Task<List<Booking>> GetAllBooking();


    }
}