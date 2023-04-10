using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Data
{
    public class MovieCatalogContext : DbContext
    {
        public MovieCatalogContext (DbContextOptions<MovieCatalogContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
