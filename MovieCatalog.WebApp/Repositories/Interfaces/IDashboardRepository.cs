using MovieCatalog.WebApp.Dtos;

namespace MovieCatalog.WebApp.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        public Task<DashboardDto> GetDashboardData();
    }
}
