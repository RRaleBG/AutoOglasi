namespace AutoOglasi.Data.Seeding;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

public class FuelTypesSeeder : ISeeder
{
    public async Task SeedAsync(AutoOglasiDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.FuelTypes.Any())
        {
            return;
        }

        var fuelTypesToSeed = new List<FuelType>()
        {
            new() { Name = "Petrol"},
            new() { Name = "Diesel"},
            new() { Name = "LPG"},
            new() { Name = "Electric" },
            new() { Name = "Hybrid (petrol/electric)" },
            new() { Name = "Hybrid (diesel/electric)" },
            new() { Name = "Other" },
        };

        await dbContext.FuelTypes.AddRangeAsync(fuelTypesToSeed);
        await dbContext.SaveChangesAsync();
    }
}