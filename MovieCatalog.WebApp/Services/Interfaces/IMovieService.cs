﻿using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<MoviePackageData> GetAllAsync(string movieTitle, string movieGenre, string movieRating, int pageNumber, int pageSize);

        public Task<List<string>> GetGenresAsync();

        public Task<List<string>> GetRatingsAsync();

        public Task<Movie> GetAsync(int id);

        public Task CreateAsync(Movie movie);

        public Task UpdateAsync(Movie movie);

        public Task DeleteAsync(int id);

        public Task<bool> Exists(int id);
    }
}
