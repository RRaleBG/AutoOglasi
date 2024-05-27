namespace AutoOglasi.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(AutoOglasiDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categoriesToSeed = new List<Category>()
            {
                new() { Name = "Saloon"},
                new() { Name = "Estate car"},
                new() { Name = "SUV/Crossover/Off-road car"},
                new() { Name = "Small car" },
                new() { Name = "Sports car/Coupe" },
                new() { Name = "Cabriolet/Roadster" },
                new() { Name = "Van/Minibus" },
                new() { Name = "Other" },
            };

            await dbContext.Categories.AddRangeAsync(categoriesToSeed);
            await dbContext.SaveChangesAsync();
        }
    }
}