using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;
using MovieCatalog.WebApp.Models;
using MovieCatalog.WebApp.Repositories.Interfaces;

namespace MovieCatalog.WebApp.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly MovieCatalogContext _context;

        public RatingRepository(MovieCatalogContext context)
            => _context = context;

        public async Task<List<Rating>> GetAllAsync()
            => await _context.Rating.ToListAsync();
    }
}
