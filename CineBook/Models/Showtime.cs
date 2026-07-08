namespace CineBook.Models
{
    public class Showtime
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
<<<<<<< HEAD
        public Movie Movie { get; set; } = null!;

        public int HallId { get; set; }
        public Hall Hall { get; set; } = null!;
=======
        public Movie Movie { get; set; }

        public int HallId { get; set; }
        public Hall Hall { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        public DateTime StartTime { get; set; }

        public decimal NormalPrice { get; set; }

        public decimal VipPrice { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
