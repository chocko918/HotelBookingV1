using FluentValidation;
using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking2.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;

        public RoomRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetAllRoom()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<List<Room>> IsRoomAvailableForBooking(int pax, DateTime checkInDate, DateTime checkOutDate)
        {
            // Ensure that check-in date is later than today's date
            if (checkInDate.Date <= DateTime.Today)
            {
                throw new ArgumentException("Check-in date must be later than today's date.");
            }

            // Ensure that check-out date is later than check-in date
            if (checkOutDate.Date <= checkInDate.Date)
            {
                throw new ArgumentException("Check-out date must be later than check-in date.");
            }
            // Check if there are any bookings for the specified room overlapping with the given date range
            var existingBookingRoom = await _context.BookingRooms.ToListAsync();

            if (existingBookingRoom.Any())
            {
                var availableRoomsQuery = _context.Rooms
                    .Where(room => room.Pax >= pax)
                    .Where(room => !(_context.BookingRooms
                        .Any(bookingRoom => bookingRoom.RoomID == room.RoomID &&
                            (bookingRoom.CheckInDate >= checkInDate && bookingRoom.CheckInDate < checkOutDate ||
                             bookingRoom.CheckOutDate > checkInDate && bookingRoom.CheckOutDate <= checkOutDate))));

                return await availableRoomsQuery.ToListAsync();

            }

            else
            {
                return await _context.Rooms.Where(room => room.Pax >= pax).ToListAsync();
            }
        }
    }
}
