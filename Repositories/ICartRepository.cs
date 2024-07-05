using HotelBooking2.Models;

namespace HotelBooking2.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddItemToCartAsync(Guid customerId, int roomId, DateTime checkInDate, DateTime checkOutDate);

        Task<List<Cart>> GetAllCartItems();

        Task DeleteCartItemByID(Guid itemID);

        Task<decimal> TotalCartPrice();

        Task DeleteAllCartItems();

        Task<List<Cart>> GetCartItemsByCustomerID(Guid customerID);

        Task<decimal> TotalCartPriceById(Guid customerID);

        Task DeleteAllCartItemsByCustomerId(Guid customerID);
    }
}