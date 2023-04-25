﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<MoviePackageData> GetAllAsync(string movieTitle, string movieGenre, string movieRating, int pageIndex, int pageSize)
        {
            MoviePackageData moviePackageData = new MoviePackageData();

            List<Movie> movies = await _context.Movie.ToListAsync();

            if (!string.IsNullOrEmpty(movieTitle))
                movies = movies
                    .Where(x => x.Title!.Contains(movieTitle))
                    .ToList();

            if (!string.IsNullOrEmpty(movieGenre))
                movies = movies
                    .Where(x => x.Genre == movieGenre)
                    .ToList();

            if (!string.IsNullOrEmpty(movieRating))
                movies = movies
                    .Where(x => x.Rating == movieRating)
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
                Rating = x.Rating,
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
                .OrderBy(x => x.Rating)
                .Select(x => x.Rating)
                .Distinct()
                .ToListAsync();

            return list!;
        }

        public async Task<Movie> GetAsync(int id)
        {
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);

            return movie!;
        }

        public async Task CreateAsync(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await GetAsync(id);

            if (movie != null)
            {
                _context.Movie.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
            => await _context.Movie?.AnyAsync(e => e.Id == id)!;
    }
}