namespace AutoOglasi.Web.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Statistics;
    using Services.Statistics.Models;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statistics)
        {
            this.statisticsService = statistics;
        }

        [HttpGet]
        public Task<StatisticsServiceModel> GetStatistics()
        {
            return this.statisticsService.TotalAsync();
        }
    }
}