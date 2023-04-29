using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Services.Interfaces
{
    public interface IRatingService
    {
        public Task<List<Rating>> GetAllAsync();
    }
}
