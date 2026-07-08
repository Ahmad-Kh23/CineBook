namespace CineBook.Models
{
    public class BookingSeat
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
<<<<<<< HEAD
        public Booking Booking { get; set; } = null!;

        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;
    }
}
=======
        public Booking Booking { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
