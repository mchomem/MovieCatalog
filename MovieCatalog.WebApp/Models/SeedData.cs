using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;

namespace MovieCatalog.WebApp.Models
{
    public static class SeedData
    {
        public static void InitializeDataBaseWithMokaData(IServiceProvider service)
        {
            using (var context = new MovieCatalogContext(service.GetRequiredService<DbContextOptions<MovieCatalogContext>>()))
            {
                if (context.Movie.Any())
                    return;

                Random r = new Random();
                string[] genres = new[] { "Action", "Adventure", "Comedy", "Drama", "Horror", "Mystery", "Thriller", "Sci-Fi" };
                string[] ratings = new[] { "G", "PG", "PG-13", "R", "NC-17"};

                for (int i = 0; i < 1000; i++)
                {
                    context.Movie.Add(new Movie()
                    {
                        Title = $"Movie title {(i + 1)}",
                        ReleaseDate = DateTime.Parse($"{r.Next(1910, 2023)}-{r.Next(1, 12)}-{r.Next(1, 28)}"),
                        Genre = genres[r.Next(0, genres.Length)],
                        Price = Convert.ToDecimal($"{r.Next(1,10)},{r.Next(0,9)}{r.Next(0, 9)}"),
                        Rating = ratings[r.Next(0, ratings.Length)]
                    });
                }
                
                context.SaveChanges();
            }
        }
    }
}
