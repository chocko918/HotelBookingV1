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
            // Check if item already exists in cart
            var existingCartItem = await _context.Carts.FirstOrDefaultAsync(c =>
                c.RoomID == roomId &&
                ((c.CheckInDate >= checkInDate && c.CheckInDate < checkOutDate) ||
                (c.CheckOutDate > checkInDate && c.CheckOutDate <= checkOutDate)));

            if (existingCartItem != null)
            {
                throw new Exception("Item already exists in cart.");
            }

            var newItem = new Cart
            {
                ItemID = Guid.NewGuid(),
                RoomID = roomId,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };

            await _context.Carts.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return newItem;
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
    }
}
