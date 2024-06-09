using System.ComponentModel.DataAnnotations;

namespace HotelBooking2.Models
{
    public class CreateCustomerDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only consist of alphabets")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

    }
}
