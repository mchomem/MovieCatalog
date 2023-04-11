using Microsoft.AspNetCore.Mvc;
using MovieCatalog.WebApp.Dtos;
using MovieCatalog.WebApp.Services.Interfaces;

namespace MovieCatalog.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardService _dashboardService;

        public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
            => View();

        public async Task<DashboardDto> GetDashboardData()
            => await _dashboardService.GetDashboardData();
    }
}
