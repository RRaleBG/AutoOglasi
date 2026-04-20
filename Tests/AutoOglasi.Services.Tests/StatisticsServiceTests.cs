namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data.Models;
using AutoOglasi.Services.Statistics;

public class StatisticsServiceTests
{
    [Fact]
    public async Task TotalAsync_ReturnsExpectedTotals()
    {
        await using var data = TestDbContextFactory.Create("statistics-tests");

        data.Users.AddRange(
            new ApplicationUser { UserName = "seller-1", Email = "seller1@test.local", FullName = "Seller One" },
            new ApplicationUser { UserName = "seller-2", Email = "seller2@test.local", FullName = "Seller Two" });

        data.Categories.AddRange(
            new Category { Name = "Sedan" },
            new Category { Name = "SUV" },
            new Category { Name = "Coupe" });

        data.Posts.AddRange(
            new Post { CreatorId = "user-1", CarId = 1, SellerName = "Seller One", SellerPhoneNumber = "111111", IsPublic = true, IsDeleted = false },
            new Post { CreatorId = "user-2", CarId = 2, SellerName = "Seller Two", SellerPhoneNumber = "222222", IsPublic = true, IsDeleted = false },
            new Post { CreatorId = "user-1", CarId = 3, SellerName = "Seller Three", SellerPhoneNumber = "333333", IsPublic = false, IsDeleted = false },
            new Post { CreatorId = "user-2", CarId = 4, SellerName = "Seller Four", SellerPhoneNumber = "444444", IsPublic = true, IsDeleted = true });

        await data.SaveChangesAsync();

        var service = new StatisticsService(data);

        var statistics = await service.TotalAsync();

        Assert.Equal(2, statistics.TotalUsers);
        Assert.Equal(2, statistics.TotalPosts);
        Assert.Equal(3, statistics.TotalCategories);
    }
}
