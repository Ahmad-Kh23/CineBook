using CineBook.Dtos.Movies;

namespace CineBook.Services.Interfaces
{
    public interface IMovieService
    {


        Task CreateMovieAsync(CreateMovieDto dto);
        Task<MoviesDto> GetAllMoviesAsync();



    }
}
