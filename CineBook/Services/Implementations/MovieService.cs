using CineBook.Data;
using CineBook.Dtos.Movies;
using CineBook.Models;
using CineBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineBook.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }
        ////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////

        public async Task<MoviesDto> GetMoviesAsync(SearchMoviesDto search)
        {
            var query = _context.Movies
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Title) && search.Title.Trim().Length >= 2) // Word start search by 2 letters aka A wont work
                                                                                             // but A Q will work getting A Quiet place for ex
            {
                var title = search.Title.Trim();

                query = query.Where(m =>
                    m.Title.StartsWith(title) ||
                    m.Title.Contains(" " + title)
                );
            }

            if (search.Genre.HasValue)
            {
                query = query.Where(m => m.Genre == search.Genre.Value);
            }

            var movies = await query
                .OrderBy(m => m.Title)
                .Select(m => new MovieListDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre.ToString(),
                    DurationMinutes = m.DurationMinutes,
                    Language = m.Language,
                    ReleaseDate = m.ReleaseDate,
                    Status = m.Status.ToString(),
                    ShowtimesCount = m.Showtimes.Count
                })
                .ToListAsync();

            return new MoviesDto
            {
                Movies = movies
            };
        }
    }
}