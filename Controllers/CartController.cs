using Azure.Core;
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
        [HttpGet("getCartItemsByCustomerID")]
        public async Task<IActionResult> GetCartItemsByCustomerID([FromQuery] GetBookingByCustomerIDRequest request)
        {
            if (Guid.TryParse(request.CustomerID, out Guid customerID))
                try
                {

                    var rooms = await _cartRepository.GetCartItemsByCustomerID(customerID);
                    var totalPrice = await _cartRepository.TotalCartPriceById(customerID);
                    return Ok(new { rooms, totalPrice });
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            else
            {
                return BadRequest("Invalid CustomerID format.");
            }
        }


        //[HttpPost("getBookingByCustomerID")]
        //public async Task<IActionResult> GetBookingByCustomerID([FromBody] GetBookingByCustomerIDRequest request)
        //{
        //    if (Guid.TryParse(request.CustomerID, out Guid customerID))
        //    {
        //        try
        //        {
        //            var bookingsByCustomerID = await _bookingRepository.GetBookingByCustomerID(customerID);
        //            return Ok(bookingsByCustomerID);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest($"Error: {ex.Message}");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Invalid CustomerID format.");
        //    }
        //}


        [HttpPost("addItemToCart")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartRequest request)
        {

            try
            {
                var cartItem = await _cartRepository.AddItemToCartAsync(request.CustomerID, request.RoomID, request.CheckInDate, request.CheckOutDate);
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

        [HttpDelete("DeleteAllCartItem")]
        public async Task<IActionResult> DeleteAllCartItems()
        {
            try
            {
                await _cartRepository.DeleteAllCartItems();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAllCartItemByCustomerId")]
        public async Task<IActionResult> DeleteAllCartItemsByCustomerId([FromQuery] GetBookingByCustomerIDRequest request)
        {
            if (Guid.TryParse(request.CustomerID, out Guid customerID))
                try
                {
                    await _cartRepository.DeleteAllCartItemsByCustomerId(customerID);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            else
            {
                return BadRequest("Invalid CustomerID format.");
            }
        }

    }
}
