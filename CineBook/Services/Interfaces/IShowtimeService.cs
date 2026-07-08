using CineBook.Dtos.Showtimes;

namespace CineBook.Services.Interfaces
{
    public interface IShowtimeService
    {
        Task<CreateShowtimeResultDto> CreateShowtimeAsync(CreateShowtimeDto dto);
        Task<CreateShowtimeFormDto?> GetCreateShowtimeFormAsync(CreateShowtimeFormRequestDto dto);
    }
}
