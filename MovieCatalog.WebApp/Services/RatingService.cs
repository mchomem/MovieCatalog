using MovieCatalog.WebApp.Models;
using MovieCatalog.WebApp.Repositories.Interfaces;
using MovieCatalog.WebApp.Services.Interfaces;

namespace MovieCatalog.WebApp.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
            => _ratingRepository = ratingRepository;

        public async Task<List<Rating>> GetAllAsync()
            => await _ratingRepository.GetAllAsync();        
    }
}
