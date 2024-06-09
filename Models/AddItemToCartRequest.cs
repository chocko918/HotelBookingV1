using System.ComponentModel.DataAnnotations;

namespace HotelBooking2.Models
{
    public class AddItemToCartRequest
    {
        public int RoomID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }
    }

}
