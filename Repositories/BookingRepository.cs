using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using HotelBooking2.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking2.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private readonly HotelBookingDbContext _context;

        public BookingRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingRoom>> GetAllBookingRoomItems()
        {
            return await _context.BookingRooms.ToListAsync();
        }

        public async Task<List<Booking>> GetAllBooking()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task ConfirmBooking()
        {
            var cart = await _context.Carts.ToListAsync();

            // Create a new Booking object
            var booking = new Booking
            {
                BookingID = Guid.NewGuid(), // Generate a new Booking ID
                CustomerID = new Guid("c54b4e24-d5af-4d88-ade5-08dc6dd9e65d"),
                TotalPrice = 5,


            };

            // Loop through each cart item
            foreach (var cartItem in cart)
            {
                // Create a BookingRoom object for each cart item
                var bookingRoom = new BookingRoom
                {
                    BookingRoomID = Guid.NewGuid() ,
                    BookingID = booking.BookingID, // Use the same Booking ID
                    RoomID = cartItem.RoomID,
                    Pax = await _context.Rooms.Where(r => r.RoomID == cartItem.RoomID).Select(r => r.Pax).FirstAsync(),
                    CheckInDate = cartItem.CheckInDate,
                    CheckOutDate = cartItem.CheckOutDate,
                };

                // Add BookingRoom to Booking
                 _context.BookingRooms.Add(bookingRoom);
                

            }

            // Save the Booking and BookingRoom entries
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Clear the cart after successful booking
            //await _context.ClearCartAsync();


        }

    }
}
