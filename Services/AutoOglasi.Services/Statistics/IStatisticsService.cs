namespace AutoOglasi.Services.Statistics
{
    using System.Threading.Tasks;
    using Models;

    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> TotalAsync();
    }
}