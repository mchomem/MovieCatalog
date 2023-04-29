using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Repositories.Interfaces
{
    public interface IRatingRepository
    {
        public Task<List<Rating>> GetAllAsync();
    }
}
