using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MovieCatalog.WebApp.Data;

namespace MovieCatalog.WebApp.Models
{
    public static class SeedData
    {
        public static void InitializeDataBaseWithMokaData(IServiceProvider service)
        {
            using (var context = new MovieCatalogContext(service.GetRequiredService<DbContextOptions<MovieCatalogContext>>()))
            {
                if(!context.Database.CanConnect())
                    context.Database.Migrate();

                if (context.Movie.Any())
                    return;

                IDbContextTransaction transaction = context.Database.BeginTransaction();

                string[] ratings = new string[]
                {
                    "G",
                    "NC-17",
                    "PG",
                    "PG-13",
                    "R"
                };

                context.Rating.AddRange
                    (
                        new Rating() { Name = ratings[0] },
                        new Rating() { Name = ratings[1] },
                        new Rating() { Name = ratings[2] },
                        new Rating() { Name = ratings[3] },
                        new Rating() { Name = ratings[4] }
                    );

                context.SaveChanges();

                Random r = new Random();
                string[] genres = new[]
                {
                    "Action",
                    "Adventure",
                    "Comedy",
                    "Drama",
                    "Horror",
                    "Mystery",
                    "Thriller",
                    "Sci-Fi"
                };

                for (int i = 0; i < 1000; i++)
                {
                    string ratingName = ratings[r.Next(0, ratings.Length)];

                    Rating rating = context.Rating
                            .Where(x => x.Name == ratingName)
                            .FirstOrDefault()!;

                    if (rating == null)
                        throw new Exception("rating cannot null");
                    
                    context.Movie.Add(new Movie()
                    {
                        Title = $"Movie title {(i + 1)}",
                        ReleaseDate = DateTime.Parse($"{r.Next(1910, 2023)}-{r.Next(1, 12)}-{r.Next(1, 28)}"),
                        Genre = genres[r.Next(0, genres.Length)],
                        Price = Convert.ToDecimal($"{r.Next(1, 10)},{r.Next(0, 9)}{r.Next(0, 9)}"),
                        Rating = rating
                    });
                }

                context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}
