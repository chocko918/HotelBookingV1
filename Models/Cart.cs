using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking2.Models
{
    public class Cart
    {
        [Required]
        public Guid ItemID { get; set; }

        [Required] 
        public int RoomID { get; set; }

        [ForeignKey ("RoomID")]
        public Room Room { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }

        public Guid CustomerID { get; set; }
        [Required]

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

    }
}
