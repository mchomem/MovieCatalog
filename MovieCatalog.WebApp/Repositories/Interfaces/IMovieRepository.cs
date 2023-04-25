using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        public Task<MoviePackageData> GetAllAsync(MovieDto filter, int pageIndex, int pageSize);

        public Task<List<string>> GetGenresAsync();

        public Task<List<string>> GetRatingsAsync();

        public Task<Movie> GetAsync(int id);

        public Task CreateAsync(Movie movie);

        public Task UpdateAsync(Movie movie);

        public Task DeleteAsync(int id);

        public Task<bool> Exists(int id);
    }
}
