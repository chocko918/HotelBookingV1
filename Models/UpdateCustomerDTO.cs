namespace HotelBooking2.Models
{
    public class UpdateCustomerDTO
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}
