namespace CineBook.Models
{
    public enum BookingStatus
    {
        Confirmed,
        Cancelled
    }

    public class Booking
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; }

        public DateTime BookingDate { get; set; }

        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }

        public List<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}