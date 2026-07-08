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

<<<<<<< HEAD
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; } = null!;
=======
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        public DateTime BookingDate { get; set; }

        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; }

        public List<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
