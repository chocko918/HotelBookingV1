using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelBooking2.Models
{
    public class BookingRoom
    {
        [Required]
        public Guid BookingRoomID { get; set; }

        [Required]
        public Guid BookingID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [Required]
        public int Pax { get; set; }

        [ForeignKey("RoomID")] // Add this attribute to specify the foreign key relationship
        [JsonIgnore]
        public Room Room { get; set; } // Navigation property for the Room entity

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [ForeignKey("BookingID")]
        public Booking Booking { get; set; }
    }

}
