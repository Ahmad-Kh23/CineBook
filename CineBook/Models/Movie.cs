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
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int DurationMinutes { get; set; }

        public Genre Genre { get; set; }

        [MaxLength(50)]
        public string Language { get; set; }

        public string PosterUrl { get; set; }

        public DateTime ReleaseDate { get; set; }

        public MovieStatus Status { get; set; }

        public List<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}