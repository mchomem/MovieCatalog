using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Models;
using MovieCatalog.WebApp.Repositories.Interfaces;
using MovieCatalog.WebApp.Services.Interfaces;

namespace MovieCatalog.WebApp.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
            => _movieRepository = movieRepository;

        public async Task<MoviePackageData> GetAllAsync(MovieDto filter, int pageNumber, int pageSize)
            => await _movieRepository.GetAllAsync(filter, pageNumber, pageSize);

        public async Task<List<string>> GetGenresAsync()
            => await _movieRepository.GetGenresAsync();

        public async Task<List<string>> GetRatingsAsync()
            => await _movieRepository.GetRatingsAsync();

        public async Task<Movie> GetAsync(int id)
            => await _movieRepository.GetAsync(id);

        public async Task CreateAsync(Movie movie)
            => await _movieRepository.CreateAsync(movie);

        public async Task UpdateAsync(Movie movie)
            => await _movieRepository.UpdateAsync(movie);

        public async Task DeleteAsync(int id)
            => await _movieRepository.DeleteAsync(id);

        public async Task<bool> Exists(int id)
            => await _movieRepository.Exists(id);
    }
}
