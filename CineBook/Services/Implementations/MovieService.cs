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
        ////////////////////////////////////////////////////////////////////////////////////////////// CREATE

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

        ////////////////////////////////////////////////////////////////////////////////////////////// SEARCH


        public async Task<MoviesDto> GetMoviesAsync(SearchMoviesDto search)
        {
            var query = _context.Movies
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Title) && search.Title.Trim().Length >= 2)
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

        ////////////////////////////////////////////////////////////////////////////////////////////// EDIT

        public async Task<UpdateMovieDto?> GetMovieForEditAsync(GetMovieForEditDto dto)
        {
            return await _context.Movies
                .AsNoTracking()
                .Where(m => m.Id == dto.Id)
                .Select(m => new UpdateMovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    DurationMinutes = m.DurationMinutes,
                    Genre = m.Genre,
                    Language = m.Language,
                    PosterUrl = m.PosterUrl,
                    ReleaseDate = m.ReleaseDate,
                    Status = m.Status
                })
                .FirstOrDefaultAsync();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////// UPDATE

        public async Task<UpdateMovieResultDto> UpdateMovieAsync(UpdateMovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(dto.Id);

            if (movie == null)
            {
                return new UpdateMovieResultDto
                {
                    Result = UpdateMovieResult.NotFound
                };
            }

            movie.Title = dto.Title;
            movie.Description = dto.Description ?? string.Empty;
            movie.DurationMinutes = dto.DurationMinutes;
            movie.Genre = dto.Genre;
            movie.Language = dto.Language;
            movie.PosterUrl = dto.PosterUrl ?? string.Empty;
            movie.ReleaseDate = dto.ReleaseDate;
            movie.Status = dto.Status;

            await _context.SaveChangesAsync();
            return new UpdateMovieResultDto
            {
                Result = UpdateMovieResult.Updated
            };
        }

        ////////////////////////////////////////////////////////////////////////////////////////////// DELETE
        public async Task<DeleteMovieResultDto> DeleteMovieAsync(DeleteMovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(dto.Id);

            if (movie == null)
            {
                return new DeleteMovieResultDto
                {
                    Result = DeleteMovieResult.NotFound
                };
            }

            var hasShowtimes = await _context.Showtimes.AnyAsync(s => s.MovieId == dto.Id);

            if (hasShowtimes)
            {
                return new DeleteMovieResultDto
                {
                    Result = DeleteMovieResult.HasShowtimes
                };
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return new DeleteMovieResultDto
            {
                Result = DeleteMovieResult.Deleted
            };
        }
    }
}
