using CineBook.Data;
using CineBook.Models;
using CineBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineBook.Controllers
{
    [Authorize]
    public class ShowtimesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowtimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Select(int id)
        {
            var showtime = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefault(s => s.Id == id);

            if (showtime == null)
            {
                return NotFound();
            }

            var seats = _context.Seats
                .Where(s => s.HallId == showtime.HallId)
                .OrderBy(s => s.RowLabel)
                .ThenBy(s => s.SeatNumber)
                .ToList();

            var takenSeatIds = _context.BookingSeats
                .Where(bs => bs.Booking.ShowtimeId == id && bs.Booking.Status == BookingStatus.Confirmed)
                .Select(bs => bs.SeatId)
                .ToList();

            var viewModel = new SeatSelectionViewModel
            {
                Showtime = showtime,
                SeatsByRow = seats
                    .GroupBy(s => s.RowLabel)
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.ToList()),
                TakenSeatIds = takenSeatIds
            };

            return View(viewModel);
        }
    }
}
