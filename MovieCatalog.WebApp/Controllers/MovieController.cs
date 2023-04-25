using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Models;
using MovieCatalog.WebApp.Services.Interfaces;

namespace MovieCatalog.WebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
            => _movieService = movieService;

        public async Task<IActionResult> Index(string titleFilter, string genreFilter, string ratingFilter, int? pageNumber = 1)
        {
            if (titleFilter != null || pageNumber < 1)
                pageNumber = 1;

            int pageSize = 10;

            MovieDto filter = new MovieDto()
            {
                Title = titleFilter,
                Genre = genreFilter,
                Rating = ratingFilter
            };

            var movieFiltersView = new MovieViewModel
            {
                MoviePackageData = await _movieService.GetAllAsync(filter, pageNumber!.Value, pageSize),
                Genres = new SelectList(await _movieService.GetGenresAsync()),
                Ratings = new SelectList(await _movieService.GetRatingsAsync()),
                PageIndex = pageNumber.Value,
            };

            return View(movieFiltersView);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetAsync(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetAsync(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating,CreatedIn,ModifiedIn")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    movie.ModifiedIn = DateTime.Now;
                    await _movieService.UpdateAsync(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _movieService.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetAsync(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
