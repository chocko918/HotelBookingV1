using HotelBooking2.Helpers;
using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using HotelBooking2.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace HotelBooking2.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private readonly HotelBookingDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingRepository(HotelBookingDbContext context)
        {
            _context = context;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<BookingRoom>> GetAllBookingRoomItems()
        {
            return await _context.BookingRooms.ToListAsync();
        }

        public async Task<List<Booking>> GetAllBooking()
        {
            //var customerId = _httpContextAccessor.HttpContext.Items["CustomerID"]?.ToString();
            //if (customerId == null)
            //{
            //    throw new Exception("Customer ID not found in context.");
            //}

            //var customerGuid = Guid.Parse(customerId);
            //return await _context.Bookings.Where(b => b.CustomerID == customerGuid).ToListAsync();

            return await _context.Bookings.ToListAsync();
        }

        //public async Task ConfirmBooking(Guid customerGuid)
        //{

        //    var cart = await _context.Carts.ToListAsync();

        //    if (cart == null || !cart.Any())
        //    {
        //        throw new Exception("Cart is empty.");
        //    }

        //    // Create a new Booking object
        //    var booking = new Booking
        //    {
        //        BookingID = Guid.NewGuid(), // Generate a new Booking ID
        //        //CustomerID = new Guid("c54b4e24-d5af-4d88-ade5-08dc6dd9e65d"),
        //        CustomerID = customerGuid,
        //        TotalPrice = cart.Sum(item => item.Price),
        //    };

        //    // Loop through each cart item
        //    foreach (var cartItem in cart)
        //    {
        //        var room = await _context.Rooms.FirstOrDefaultAsync(c => c.RoomID == cartItem.RoomID);

        //        if (room == null)
        //        {
        //            throw new Exception("Room not found.");
        //        }

        //        // Create a BookingRoom object for each cart item
        //        var bookingRoom = new BookingRoom
        //        {
        //            BookingRoomID = Guid.NewGuid() ,
        //            BookingID = booking.BookingID, // Use the same Booking ID
        //            RoomID = cartItem.RoomID,
        //            Pax = room.Pax,
        //            Price = room.Price,
        //            Name = room.Name,
        //            CheckInDate = cartItem.CheckInDate.Date,
        //            CheckOutDate = cartItem.CheckOutDate.Date,
        //        };

        //        // Add BookingRoom to Booking
        //         _context.BookingRooms.Add(bookingRoom);


        //    }

        //    // Save the Booking and BookingRoom entries
        //    _context.Bookings.Add(booking);
        //    await _context.SaveChangesAsync();

        //    // Clear the cart after successful booking
        //    //await _context.ClearCartAsync();


        //}

        public async Task ConfirmBooking(Guid customerGuid)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cart = await _context.Carts.ToListAsync();

                    if (cart == null || !cart.Any())
                    {
                        throw new Exception("Cart is empty.");
                    }

                    // Create a new Booking object
                    var booking = new Booking
                    {
                        BookingID = Guid.NewGuid(), // Generate a new Booking ID
                        CustomerID = customerGuid,
                        TotalPrice = cart.Sum(item => item.Price),
                    };

                    // Add the booking to the context
                    _context.Bookings.Add(booking);

                    // Loop through each cart item
                    foreach (var cartItem in cart)
                    {
                        var room = await _context.Rooms.FirstOrDefaultAsync(c => c.RoomID == cartItem.RoomID);

                        if (room == null)
                        {
                            throw new Exception("Room not found.");
                        }

                        // Create a BookingRoom object for each cart item
                        var bookingRoom = new BookingRoom
                        {
                            BookingRoomID = Guid.NewGuid(),
                            BookingID = booking.BookingID, // Use the same Booking ID
                            RoomID = cartItem.RoomID,
                            Pax = room.Pax,
                            Price = room.Price,
                            Name = room.Name,
                            CheckInDate = cartItem.CheckInDate.Date,
                            CheckOutDate = cartItem.CheckOutDate.Date,
                        };

                        // Add BookingRoom to context
                        _context.BookingRooms.Add(bookingRoom);
                    }

                    // Save all changes within the transaction
                    await _context.SaveChangesAsync();

                    // Clear the cart after successful booking (uncomment if needed)
                    var allCartItems = await _context.Carts.ToListAsync();
                    _context.Carts.RemoveRange(allCartItems);
                    await _context.SaveChangesAsync();

                    // Commit the transaction
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Inner Exception: {ex}");
                    throw; // Re-throw the exception to be caught by the controller
                }
            }
        }

        public async Task<List<BookedRoomDTO>> GetBookingByCustomerID(Guid customerID)
        {
            var bookedRooms = await _context.Bookings
                                             .Where(b => b.CustomerID == customerID)
                                             .Include(b => b.BookingRooms)
                                             .SelectMany(b => b.BookingRooms)
                                             .Select(br => new BookedRoomDTO
                                             {
                                                 BookingRoomID = br.BookingRoomID,
                                                 BookingID = br.BookingID,
                                                 RoomID = br.RoomID,
                                                 Name = br.Name,
                                                 Pax = br.Pax,
                                                 CheckInDate = br.CheckInDate,
                                                 CheckOutDate = br.CheckOutDate
                                             })
                                             .ToListAsync();


            return bookedRooms;
        }

    }
}
