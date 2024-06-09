using HotelBooking2.Models;

namespace HotelBooking2.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoom();

        Task<List<Room>> IsRoomAvailableForBooking(int pax, DateTime checkInDate, DateTime checkOutDate);
    }
}