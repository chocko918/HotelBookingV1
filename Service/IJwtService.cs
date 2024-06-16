using HotelBooking2.Models;
using System.Security.Claims;

namespace HotelBooking2.Service
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(Customer user);
    }
}