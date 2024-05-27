namespace AutoOglasi.Data.Seeding;

using System;
using System.Threading.Tasks;

public interface ISeeder
{
    Task SeedAsync(AutoOglasiDbContext dbContext, IServiceProvider serviceProvider);
}
