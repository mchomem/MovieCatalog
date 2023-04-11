using MovieCatalog.WebApp.Dtos;

namespace MovieCatalog.WebApp.Services.Interfaces
{
    public interface IDashboardService
    {
        public Task<DashboardDto> GetDashboardData();
    }
}
