using CineBook.Data;
using CineBook.Dtos.Movies;
using CineBook.Models;
using CineBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CineBook.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin/Movies")]
    public class AdminMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index(SearchMoviesDto search)
        {
            var query = _context.Movies
                .Include(m => m.Showtimes)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                query = query.Where(m => m.Title.Contains(search.Title));
            }

            if (search.Genre.HasValue)
            {
                query = query.Where(m => m.Genre == search.Genre.Value);
            }

            var movies = query
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
                .ToList();

            var viewModel = new AdminMovieIndexViewModel
            {
                Search = search,
                Movies = movies
            };

            return View("~/Views/Admin/Movies/Index.cshtml", viewModel);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Movies/Create.cshtml", new CreateMovieDto());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Movies/Create.cshtml", dto);
            }

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
            _context.SaveChanges();

            TempData["AdminMovieMessage"] = "Movie added successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            var dto = new CreateMovieDto
            {
                Title = movie.Title,
                Description = movie.Description,
                DurationMinutes = movie.DurationMinutes,
                Genre = movie.Genre,
                Language = movie.Language,
                PosterUrl = movie.PosterUrl,
                ReleaseDate = movie.ReleaseDate,
                Status = movie.Status
            };

            return View("~/Views/Admin/Movies/Edit.cshtml", dto);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateMovieDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Admin/Movies/Edit.cshtml", dto);
            }

            var movie = _context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = dto.Title;
            movie.Description = dto.Description ?? string.Empty;
            movie.DurationMinutes = dto.DurationMinutes;
            movie.Genre = dto.Genre;
            movie.Language = dto.Language;
            movie.PosterUrl = dto.PosterUrl ?? string.Empty;
            movie.ReleaseDate = dto.ReleaseDate;
            movie.Status = dto.Status;

            _context.SaveChanges();

            TempData["AdminMovieMessage"] = "Movie updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Showtimes)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            if (movie.Showtimes.Any())
            {
                TempData["AdminMovieError"] = "This movie has showtimes and cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            TempData["AdminMovieMessage"] = "Movie deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
