namespace AutoOglasi.Data.Seeding;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

public class ExtraTypesSeeder : ISeeder
{
    public async Task SeedAsync(AutoOglasiDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.ExtraTypes.Any())
        {
            return;
        }
        
        var extraTypesToSeed = new List<ExtraType>()
        {
            new() { Name = "Comfort"},
            new() { Name = "Safety"},
            new() { Name = "Other"},
        };

        await dbContext.ExtraTypes.AddRangeAsync(extraTypesToSeed);
        await dbContext.SaveChangesAsync();
    }
}