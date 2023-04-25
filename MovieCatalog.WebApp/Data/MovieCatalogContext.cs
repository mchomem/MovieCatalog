using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Data
{
    public class MovieCatalogContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; } = default!;

        public MovieCatalogContext(DbContextOptions<MovieCatalogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Movie>()
                .Property(x => x.CreatedIn)
                .HasDefaultValueSql("getdate()");
        }
    }
}
