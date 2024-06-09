using HotelBooking2.Models;

namespace HotelBooking2.Service
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(Customer customer);
    }
}