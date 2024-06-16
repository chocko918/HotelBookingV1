using System.ComponentModel.DataAnnotations;
namespace HotelBooking2.Models
{
    public class BookedRoomDTO
    {
        public Guid BookingRoomID { get; set; }
        public Guid BookingID { get; set; }
        public int RoomID { get; set; }
        public int Pax { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
