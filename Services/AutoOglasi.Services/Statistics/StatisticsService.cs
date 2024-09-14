namespace AutoOglasi.Services.Statistics
{
    using System.Linq;
    using Data;
    using Models;

    public class StatisticsService : IStatisticsService
    {
        private readonly AutoOglasiDbContext _dataContext;

        public StatisticsService(AutoOglasiDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public StatisticsServiceModel Total()
        {
            var totalUsers = _dataContext.Users.Count();
            var totalPosts = _dataContext.Posts.Count(p => !p.IsDeleted && p.IsPublic);
            var totalCategories = _dataContext.Categories.Count();


            return new StatisticsServiceModel
            {
                TotalUsers = totalUsers,
                TotalPosts = totalPosts,
                TotalCategories = totalCategories,
            };
        }
    }
}
