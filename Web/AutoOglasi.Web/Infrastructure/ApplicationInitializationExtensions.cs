namespace AutoOglasi.Web.Infrastructure;

using System;
using System.Threading.Tasks;
using AutoOglasi.Data;
using AutoOglasi.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public static class ApplicationInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        await using var serviceScope = app.Services.CreateAsyncScope();
        var logger = serviceScope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("DatabaseInitialization");

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AutoOglasiDbContext>();

        logger.LogInformation("Applying database migrations.");
        await dbContext.Database.MigrateAsync();

        logger.LogInformation("Applying database seed data.");
        await new AutoOglasiDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider);
    }
}
