using CineBook.Data;
using CineBook.Dtos.Movies;
using CineBook.Models;

namespace CineBook.Services.Implementations
{
    public class MovieService
    {

        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMovieAsync(CreateMovieDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Description = dto.Description ?? string.Empty,
                DurationMinutes = dto.DurationMinutes,
                Genre = dto.Genre,
                Language = dto.Language,
                PosterUrl = dto.PosterUrl ?? string.Empty,
                ReleaseDate = dto.ReleaseDate,
                Status = dto.Status
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

    }
}
