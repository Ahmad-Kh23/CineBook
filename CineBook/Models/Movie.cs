using System.ComponentModel.DataAnnotations;

namespace CineBook.Models
{
    public enum Genre
    {
        Action,
        Comedy,
        Drama,
        Horror,
        Romance,
        SciFi,
        Animation,
        Thriller
    }

    public enum MovieStatus
    {
        NowShowing,
        ComingSoon
    }

    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
<<<<<<< HEAD
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
=======
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        public int DurationMinutes { get; set; }

        public Genre Genre { get; set; }

        [MaxLength(50)]
<<<<<<< HEAD
        public string Language { get; set; } = string.Empty;

        public string PosterUrl { get; set; } = string.Empty;
=======
        public string Language { get; set; }

        public string PosterUrl { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        public DateTime ReleaseDate { get; set; }

        public MovieStatus Status { get; set; }

        public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
