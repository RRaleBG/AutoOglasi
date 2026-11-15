namespace AutoOglasi.Web.Infrastructure;
using AutoOglasi.Data;
using AutoOglasi.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public static class ApplicationInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        await using var serviceScope = app.Services.CreateAsyncScope();
        var logger = serviceScope.ServiceProvider
            .GetRequiredService<ILoggerFactory>()
            .CreateLogger("DatabaseInitialization");

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AutoOglasiDbContext>();

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (!pendingMigrations.Any())
        {
            logger.LogInformation("No pending database migrations.");
        }
        else if (await TableExistsAsync(dbContext, "AspNetRoles"))
        {
            logger.LogWarning("Detected existing PostgreSQL schema without EF migration history. Skipping automatic migration execution.");
        }
        else
        {
            logger.LogInformation("Applying database migrations.");
            await dbContext.Database.MigrateAsync();
        }

        logger.LogInformation("Applying database seed data.");
       await new AutoOglasiDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider);
    }

    private static async Task<bool> TableExistsAsync(AutoOglasiDbContext dbContext, string tableName)
    {
        var connection = dbContext.Database.GetDbConnection();
        var shouldCloseConnection = connection.State != ConnectionState.Open;

        if (shouldCloseConnection)
        {
            try
            {
                await connection.OpenAsync();
            }
            catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.InvalidCatalogName)
            {
                return false;
            }
        }

        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_schema = ANY(current_schemas(false)) AND lower(table_name) = lower(@tableName));";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "tableName";
            parameter.Value = tableName;
            command.Parameters.Add(parameter);

            var result = await command.ExecuteScalarAsync();
            return result is true;
        }
        finally
        {
            if (shouldCloseConnection)
            {
                await connection.CloseAsync();
            }
        }
    }
}
