using HotelBooking2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking2.Controllers
{
    public class RoomController: Controller
    {

        private readonly IRoomRepository _roomRepository;

        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("Rooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetAllRoom();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("available-rooms")]
        public async Task<IActionResult> GetAvailableRooms(int pax, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                var availableRooms = await _roomRepository.IsRoomAvailableForBooking( pax, checkInDate, checkOutDate);

                if (availableRooms.Any())
                {
                    return Ok(availableRooms); // Return list of available rooms (status code 200)
                }
                else
                {
                    return BadRequest(new { Message = "No available rooms for booking." }); // No rooms found (status code 404)
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }



    }
}
