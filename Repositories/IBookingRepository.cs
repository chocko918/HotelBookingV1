
using HotelBooking2.Models;
using System.Security.Claims;

namespace HotelBooking2.Repositories
{
    public interface IBookingRepository
    {
        Task ConfirmBooking(Guid customerGuid);

        Task<List<BookingRoom>> GetAllBookingRoomItems();

        Task<List<Booking>> GetAllBooking();

        Task<List<BookedRoomDTO>> GetBookingByCustomerID(Guid customerID);


    }
}