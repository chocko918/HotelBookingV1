using System.ComponentModel.DataAnnotations;

namespace HotelBooking2.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only consist of alphabets")]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Cart> Carts { get; set; }
    }
}
