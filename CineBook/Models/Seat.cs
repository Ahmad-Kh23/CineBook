using System.ComponentModel.DataAnnotations;

namespace CineBook.Models
{
    public enum SeatType
    {
        Normal,
        VIP
    }

    public class Seat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
<<<<<<< HEAD
        public string RowLabel { get; set; } = string.Empty;
=======
        public string RowLabel { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        public int SeatNumber { get; set; }

        public SeatType SeatType { get; set; }

        public int HallId { get; set; }
<<<<<<< HEAD
        public Hall Hall { get; set; } = null!;

        public List<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}
=======
        public Hall Hall { get; set; }

        public List<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();
    }
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
