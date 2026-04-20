namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data;
using Microsoft.EntityFrameworkCore;

internal static class TestDbContextFactory
{
    public static AutoOglasiDbContext Create(string databaseNamePrefix)
    {
        var options = new DbContextOptionsBuilder<AutoOglasiDbContext>()
            .UseInMemoryDatabase($"{databaseNamePrefix}-{Guid.NewGuid()}")
            .Options;

        return new AutoOglasiDbContext(options);
    }
}
