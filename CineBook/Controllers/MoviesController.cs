using CineBook.Data;
using CineBook.Models;
using CineBook.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineBook.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies
                .OrderBy(m => m.Title)
                .ToList();

            var viewModel = new MovieIndexViewModel
            {
                NowShowingMovies = movies
                    .Where(m => m.Status == MovieStatus.NowShowing)
                    .ToList(),
                ComingSoonMovies = movies
                    .Where(m => m.Status == MovieStatus.ComingSoon)
                    .ToList()
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Showtimes.OrderBy(s => s.StartTime))
                    .ThenInclude(s => s.Hall)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}
