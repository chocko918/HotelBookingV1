using HotelBooking2.Models;

namespace HotelBooking2.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddItemToCartAsync(int roomId, DateTime checkInDate, DateTime checkOutDate);

        Task<List<Cart>> GetAllCartItems();

        Task DeleteCartItemByID(Guid itemID);

        Task<decimal> TotalCartPrice();
    }
}