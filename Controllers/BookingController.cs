using Azure.Core;
using HotelBooking2.Models;
using HotelBooking2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HotelBooking2.Controllers
{
    public class BookingController: Controller
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository,ICartRepository cartRepository)
        {
            _bookingRepository = bookingRepository;
            
        }

        [HttpGet("getAllBookedRooms")]
        public async Task<IActionResult> GetAllBookingRoomItems()
        {
            try
            {
                var bookingRooms = await _bookingRepository.GetAllBookingRoomItems();
                return Ok(bookingRooms);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("getAllCustomerBookings")]
        public async Task<IActionResult> GetAllBooking()
        {
            try
            {
                var bookings = await _bookingRepository.GetAllBooking();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

     

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] GuidRequest request)
        {
            if (Guid.TryParse(request.CustomerID, out Guid customerID))
            {
                try
                {
                    Console.WriteLine($"Received request to confirm booking for customerID: {customerID}");
                    await _bookingRepository.ConfirmBooking(customerID);

                    var response = new
                    {
                        Message = "Booking confirmed successfully!",
                        Success = true
                    };

                    // Serialize the response object to JSON and return it
                    return Ok(JsonSerializer.Serialize(response));

                    //return Ok("Booking confirmed successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Outer Exception: {ex}");

                    // Extract and return the inner exception message if available
                    string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    return StatusCode(500, $"An error occurred while confirming booking: {errorMessage}");
                }
            }
            else
            {
                return BadRequest("Invalid CustomerID format.");
            }
        }

        [HttpPost("getBookingByCustomerID")]
        public async Task<IActionResult> GetBookingByCustomerID([FromBody] GetBookingByCustomerIDRequest request)
        {
            if (Guid.TryParse(request.CustomerID, out Guid customerID))
            {
                try
                {
                    var bookingsByCustomerID = await _bookingRepository.GetBookingByCustomerID(customerID);
                    return Ok(bookingsByCustomerID);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Invalid CustomerID format.");
            }
        }





    }
}
