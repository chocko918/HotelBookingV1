using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelBooking2.Models
{
    public class Room
    {
        [Required]
        public int RoomID { get; set; }

        // Specify default values for Name, Pax, and Price using data annotations
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        public int Pax { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [JsonIgnore]
        public ICollection<BookingRoom> BookingRooms { get; set; }
    }
}
