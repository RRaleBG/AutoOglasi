namespace AutoOglasi.Services.Tests;

using AutoOglasi.Services.Statistics;
using AutoOglasi.Services.Statistics.Models;
using AutoOglasi.Web.Controllers.Api;

public class StatisticsControllerTests
{
    [Fact]
    public async Task GetStatistics_ReturnsStatisticsFromService()
    {
        var expectedStatistics = new StatisticsServiceModel
        {
            TotalUsers = 7,
            TotalPosts = 13,
            TotalCategories = 5,
        };

        var controller = new StatisticsController(new StubStatisticsService(expectedStatistics));

        var result = await controller.GetStatistics();

        Assert.Same(expectedStatistics, result);
    }

    private sealed class StubStatisticsService : IStatisticsService
    {
        private readonly StatisticsServiceModel statistics;

        public StubStatisticsService(StatisticsServiceModel statistics)
        {
            this.statistics = statistics;
        }

        public Task<StatisticsServiceModel> TotalAsync()
        {
            return Task.FromResult(this.statistics);
        }
    }
}
