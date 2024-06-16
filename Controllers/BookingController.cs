using HotelBooking2.Models;
using HotelBooking2.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // POST api/booking/confirm
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking(Guid CustomerGuID)
        {
            try
            {
                await _bookingRepository.ConfirmBooking(CustomerGuID);
                return Ok("Booking confirmed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Outer Exception: {ex}");

                // Extract and return the inner exception message if available
                string errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return StatusCode(500, $"An error occurred while confirming booking: {errorMessage}");
            }
        }

        [HttpPost("getBookingByCustomerID")]
        public async Task<IActionResult> GetBookingByCustomerID(Guid customerID)
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




    }
}
