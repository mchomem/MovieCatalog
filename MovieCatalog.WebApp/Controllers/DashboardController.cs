using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;
using MovieCatalog.WebApp.Dtos;

namespace MovieCatalog.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly MovieCatalogContext _context;

        public DashboardController(ILogger<DashboardController> logger, MovieCatalogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
            => View();

        public async Task<int> GetCount()
            => await _context.Movie.CountAsync();

        public async Task<DashboardDto> GetDashboardData()
        {
            // Test delay response of dashboard.
            // Thread.Sleep(5000);

            DashboardDto dashboardData = new DashboardDto();
            dashboardData.TotalMovies = await _context.Movie.CountAsync();
            dashboardData.MoviesRating = await _context.Movie
                .GroupBy(x => x.Rating)
                .Select(x => new MovieRatingDto()
                {
                    Rating = x.Key,
                    TotalMovies = x.Count()
                })
                .ToListAsync();

            dashboardData.LastMovieAdded =
                await _context.Movie
                    .OrderByDescending(x => x.Id)
                    .Select(x => new MovieDto()
                    {
                        Title = x.Title,
                        ReleaseDate = x.ReleaseDate,
                        Genre = x.Genre,
                        Rating = x.Rating
                    })
                    .FirstOrDefaultAsync();

            return dashboardData;
        }
    }
}
