using HotelBooking2.Models;
using HotelBooking2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking2.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("getAllCartItems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                var rooms = await _cartRepository.GetAllCartItems();
                var totalPrice = await _cartRepository.TotalCartPrice();
                return Ok(new { rooms, totalPrice });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("addItemToCart")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartRequest request)
        {

            try
            {
                var cartItem = await _cartRepository.AddItemToCartAsync(request.RoomID, request.CheckInDate, request.CheckOutDate);
                //var totalPrice = await _cartRepository.TotalCartPrice();
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpDelete("DeleteCartItem")]
        public async Task<IActionResult> DeleteCartItem([FromQuery] Guid itemID)
        {
            try
            {
                await _cartRepository.DeleteCartItemByID(itemID);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


    }
}
