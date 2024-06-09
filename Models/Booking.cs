using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking2.Models
{
    public class Booking
    {
        [Required]
        public Guid BookingID { get; set; }
        [Required]
        public Guid CustomerID { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        public ICollection<BookingRoom> BookingRooms { get; set; }
    }
}
