using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;
using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieCatalogContext _context;

        public MovieController(MovieCatalogContext context)
            => _context = context;        

        public async Task<IActionResult> Index(string movieTitle, string movieGenre, string movieRating)
        {
            if (_context.Model == null)
                return Problem("Entity set 'MovieCatalogoContext.Movie' is null");

            IQueryable<string> ratings = (from m in _context.Movie
                                          orderby m.Rating
                                          select m.Rating).Distinct();

            // Use LINQ to get list of genres (used on database).
            IQueryable<string> genreQuery = (from m in _context.Movie
                                             orderby m.Genre
                                             select m.Genre).Distinct();

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(movieTitle))
                movies = movies.Where(x => x.Title!.Contains(movieTitle));

            if (!string.IsNullOrEmpty(movieGenre))
                movies = movies.Where(x => x.Genre == movieGenre);

            if (!string.IsNullOrEmpty(movieRating))
                movies = movies.Where(x => x.Rating == movieRating);

            var movieFiltersView = new MovieFiltersViewModel
            {
                Movies = await movies.ToListAsync(),
                Genres = new SelectList(await genreQuery.ToListAsync()),
                Ratings = new SelectList(await ratings.ToListAsync())
            };

            return View(movieFiltersView);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

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
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'MovieCatalogContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
