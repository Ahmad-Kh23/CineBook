using CineBook.Data;
using CineBook.Dtos.Movies;
using CineBook.Models;
using CineBook.Services.Interfaces;
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
        private readonly IMovieService _movieService;

        public AdminMoviesController(ApplicationDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(SearchMoviesDto search)
        {
            var moviesDto = await _movieService.GetMoviesAsync(search);

            var viewModel = new AdminMovieIndexViewModel
            {
                Search = search,
                Movies = moviesDto.Movies
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
