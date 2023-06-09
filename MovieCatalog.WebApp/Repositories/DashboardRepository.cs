﻿using Microsoft.EntityFrameworkCore;
using MovieCatalog.WebApp.Data;
using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Repositories.Interfaces;

namespace MovieCatalog.WebApp.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MovieCatalogContext _context;

        public DashboardRepository(MovieCatalogContext context)
            => _context = context;

        public async Task<DashboardDto> GetDashboardData()
        {
#if DEBUG
            // Test delay response of dashboard.
            Thread.Sleep(1000);
#endif

            DashboardDto dashboardData = new DashboardDto();
            dashboardData.TotalMovies = await _context.Movie.CountAsync();
            dashboardData.MoviesRating = await _context.Movie
                .GroupBy(x => x.Rating.Name)
                .Select(x => new MovieRatingDto()
                {
                    Rating = x.Key,
                    TotalMovies = x.Count()
                })
                .ToListAsync();

            dashboardData.FirstMovieAdded
                = await _context.Movie
                    .Select(x => new MovieDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ReleaseDate = x.ReleaseDate,
                        Genre = x.Genre,
                        Rating = x.Rating!
                    })
                    .FirstOrDefaultAsync();

            dashboardData.LastMovieAdded =
                await _context.Movie
                    .OrderByDescending(x => x.Id)
                    .Select(x => new MovieDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        ReleaseDate = x.ReleaseDate,
                        Genre = x.Genre,
                        Rating = x.Rating!
                    })
                    .FirstOrDefaultAsync();

            return dashboardData;
        }
    }
}
