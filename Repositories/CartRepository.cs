using HotelBooking2.Infrastructure;
using HotelBooking2.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking2.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly HotelBookingDbContext _context;

        public CartRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cart>> GetAllCartItems()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> AddItemToCartAsync(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            // Ensure that only the date part is considered
            checkInDate = checkInDate.Date;
            checkOutDate = checkOutDate.Date;
            // Check if item already exists in cart
            var existingCartItem = await _context.Carts.FirstOrDefaultAsync(c =>
                c.RoomID == roomId &&
                ((c.CheckInDate.Date >= checkInDate && c.CheckInDate.Date < checkOutDate) ||
                (c.CheckOutDate.Date > checkInDate && c.CheckOutDate.Date <= checkOutDate)));

            if (existingCartItem != null)
            {
                throw new Exception("Item already exists in cart.");
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(c => c.RoomID == roomId);
            if (room == null)
            {
                throw new Exception("Room not found.");
            }
            var newItem = new Cart
            {
                ItemID = Guid.NewGuid(),
                RoomID = roomId,
                Name = room.Name,
                Price = room.Price,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
            };

            await _context.Carts.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem;
        }

        public async Task<decimal> TotalCartPrice()
        {
            var allCartItems = await _context.Carts.ToListAsync();
            // Calculate the total price by summing up the Price of each cart item
            decimal totalPrice = allCartItems.Sum(cartItem => cartItem.Price);

            // Return the total price
            return totalPrice;
        }

        public async Task DeleteCartItemByID(Guid itemID)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.ItemID == itemID);
            if (cartItem != null)
            {
                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();

            }
        }

        public async Task DeleteAllCartItems()
        {
            var allCartItems = await _context.Carts.ToListAsync();
            _context.Carts.RemoveRange(allCartItems);
            await _context.SaveChangesAsync();
        }


    }
}
