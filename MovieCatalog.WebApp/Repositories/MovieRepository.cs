using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;
using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Models;
using MovieCatalog.WebApp.Repositories.Interfaces;

namespace MovieCatalog.WebApp.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieCatalogContext _context;

        public MovieRepository(MovieCatalogContext context)
            => _context = context;

        public async Task<MoviePackageData> GetAllAsync(MovieDto filter, int pageIndex, int pageSize)
        {
            MoviePackageData moviePackageData = new MoviePackageData();

            List<Movie> movies = await _context.Movie
                .Include(x => x.Rating)
                .ToListAsync();

            if (!string.IsNullOrEmpty(filter.Title))
                movies = movies
                    .Where(x => x.Title!.Contains(filter.Title))
                    .ToList();

            if (!string.IsNullOrEmpty(filter.Genre))
                movies = movies
                    .Where(x => x.Genre == filter.Genre)
                    .ToList();

            if (!string.IsNullOrEmpty(filter.Rating.Name))
                movies = movies
                    .Where(x => x.Rating.Name == filter.Rating.Name)
                    .ToList();

            var count = movies.Count();

            moviePackageData.TotalPageMovie = (int)Math.Ceiling(count / (double)pageSize);
            moviePackageData.TotalRecordsFounded = count;

            movies = movies
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            moviePackageData.Movies = movies.Select(x => new MovieDto()
            {
                Id = x.Id,
                Title = x.Title,
                ReleaseDate = x.ReleaseDate,
                Genre = x.Genre,
                Price = x.Price,
                Rating = x.Rating!,
            })
            .ToList();

            return moviePackageData;
        }

        public async Task<List<string>> GetGenresAsync()
        {
            var list = await _context.Movie
                .OrderBy(x => x.Genre)
                .Select(x => x.Genre)
                .Distinct()
                .ToListAsync();

            return list!;
        }

        public async Task<List<string>> GetRatingsAsync()
        {
            var list = await _context.Movie
                .OrderBy(x => x.Rating!.Name)
                .Select(x => x.Rating!.Name)
                .Distinct()
                .ToListAsync();

            return list!;
        }

        public async Task<Movie> GetAsync(int id)
        {
            var movie = await _context.Movie
                .Include(x => x.Rating)
                .FirstOrDefaultAsync(m => m.Id == id);

            return movie!;
        }

        public async Task CreateAsync(Movie movie)
        {
            _context.Add(movie);

            Rating rating = (await _context.Rating
                .Where(x => x.Id == movie.RatingId)
                .FirstOrDefaultAsync())!;

            if (rating is null)
                throw new Exception("Ratins cannot null in Movie.");

            movie.Rating = rating;

            _context.Entry(movie.Rating!).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Update(movie);

            Rating rating = (await _context.Rating
                .Where(x => x.Id == movie.RatingId)
                .FirstOrDefaultAsync())!;

            if (rating is null)
                throw new Exception("Ratins cannot null in Movie.");

            movie.Rating = rating;

            _context.Entry(movie.Rating!).State = EntityState.Unchanged;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await GetAsync(id);

            if (movie != null)
            {
                _context.Movie.Remove(movie);
                _context.Entry(movie.Rating!).State = EntityState.Unchanged;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
            => await _context.Movie?.AnyAsync(e => e.Id == id)!;
    }
}
