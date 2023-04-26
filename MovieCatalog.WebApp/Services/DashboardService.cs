using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Repositories.Interfaces;
using MovieCatalog.WebApp.Services.Interfaces;

namespace MovieCatalog.WebApp.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
            => _dashboardRepository = dashboardRepository;

        public async Task<DashboardDto> GetDashboardData()
            => await _dashboardRepository.GetDashboardData();
    }
}
