namespace AutoOglasi.Services.Statistics
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Threading.Tasks;

    public class StatisticsService : IStatisticsService
    {
        private readonly AutoOglasiDbContext dataContext;

        public StatisticsService(AutoOglasiDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<StatisticsServiceModel> TotalAsync()
        {
            var totalUsers = await this.dataContext.Users.CountAsync();
            var totalPosts = await this.dataContext.Posts.CountAsync(p => !p.IsDeleted && p.IsPublic);
            var totalCategories = await this.dataContext.Categories.CountAsync();

            return new StatisticsServiceModel
            {
                TotalUsers = totalUsers,
                TotalPosts = totalPosts,
                TotalCategories = totalCategories,
            };
        }
    }
}
