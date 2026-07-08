using System.Security.Claims;
using CineBook.Data;
using CineBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineBook.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int showtimeId, List<int> seatIds)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Challenge();
            }

            if (seatIds == null || !seatIds.Any())
            {
                TempData["SeatError"] = "Please select at least one seat.";
                return RedirectToAction("Select", "Showtimes", new { id = showtimeId });
            }

            seatIds = seatIds.Distinct().ToList();

            var showtime = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefault(s => s.Id == showtimeId);

            if (showtime == null)
            {
                return NotFound();
            }

            var seats = _context.Seats
                .Where(s => seatIds.Contains(s.Id) && s.HallId == showtime.HallId)
                .ToList();

            if (seats.Count != seatIds.Count)
            {
                TempData["SeatError"] = "One or more selected seats are not valid for this showtime.";
                return RedirectToAction("Select", "Showtimes", new { id = showtimeId });
            }

            var takenSeatIds = _context.BookingSeats
                .Where(bs => bs.Booking.ShowtimeId == showtimeId && bs.Booking.Status == BookingStatus.Confirmed)
                .Select(bs => bs.SeatId)
                .ToList();

            if (seatIds.Any(id => takenSeatIds.Contains(id)))
            {
                TempData["SeatError"] = "One or more selected seats were just booked. Please choose different seats.";
                return RedirectToAction("Select", "Showtimes", new { id = showtimeId });
            }

            var totalPrice = seats.Sum(seat =>
                seat.SeatType == SeatType.VIP ? showtime.VipPrice : showtime.NormalPrice);

            var booking = new Booking
            {
                UserId = userId,
                ShowtimeId = showtimeId,
                BookingDate = DateTime.Now,
                TotalPrice = totalPrice,
                Status = BookingStatus.Confirmed,
                BookingSeats = seats.Select(seat => new BookingSeat
                {
                    SeatId = seat.Id
                }).ToList()
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return RedirectToAction("Confirmation", new { id = booking.Id });
        }

        public IActionResult Confirmation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Challenge();
            }

            var booking = _context.Bookings
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Hall)
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .FirstOrDefault(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            if (booking.UserId != userId)
            {
                return Forbid();
            }

            return View(booking);
        }

        public IActionResult MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Challenge();
            }

            var bookings = _context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Movie)
                .Include(b => b.Showtime)
                    .ThenInclude(s => s.Hall)
                .Include(b => b.BookingSeats)
                    .ThenInclude(bs => bs.Seat)
                .OrderByDescending(b => b.BookingDate)
                .ToList();

            return View(bookings);
        }
    }
}
