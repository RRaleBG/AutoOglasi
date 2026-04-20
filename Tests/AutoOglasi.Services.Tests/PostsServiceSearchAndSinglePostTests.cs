namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data;
using AutoOglasi.Data.Models;
using AutoOglasi.Services.Cars;
using AutoOglasi.Services.Cars.Models;
using AutoOglasi.Services.Images;
using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Posts.Models;

public class PostsServiceSearchAndSinglePostTests
{
    [Fact]
    public async Task GetMatchingPostsAsync_ReturnsMatchingPublicPosts()
    {
        await using var data = TestDbContextFactory.Create("posts-service-search");
        var matchingPost = CreatePost("Audi", "A4", "quattro diesel", 10000, true, false);
        var nonPublicPost = CreatePost("Audi", "A6", "quattro premium", 12000, false, false);
        var deletedPost = CreatePost("Audi", "A3", "quattro city", 9000, true, true);
        data.Posts.AddRange(matchingPost, nonPublicPost, deletedPost);
        await data.SaveChangesAsync();

        var service = CreateService(data);
        var search = new SearchPostDTO
        {
            Car = new SearchCarInputModelDTO
            {
                TextSearchTerm = "audi",
            },
        };

        var result = (await service.GetMatchingPostsAsync(search, 0)).ToList();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetMatchingPostsAsync_ReturnsPostsWithSelectedExtras()
    {
        await using var data = TestDbContextFactory.Create("posts-service-search");
        var comfortType = new ExtraType { Name = "Comfort" };
        var climatronic = new Extra { Name = "Climatronic", Type = comfortType, TypeId = 1 };
        data.Extras.Add(climatronic);

        var matchingPost = CreatePost("Audi", "A4", "with extra", 10000, true, false, extras: [climatronic]);
        var nonMatchingPost = CreatePost("Audi", "A6", "without extra", 11000, true, false);
        data.Posts.AddRange(matchingPost, nonMatchingPost);
        await data.SaveChangesAsync();

        var service = CreateService(data);
        var search = new SearchPostDTO
        {
            Car = new SearchCarInputModelDTO(),
            SelectedExtrasIds = [climatronic.Id],
        };

        var result = (await service.GetMatchingPostsAsync(search, 0)).ToList();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetMatchingPostsAsync_ThrowsWhenNoPostsMatch()
    {
        await using var data = TestDbContextFactory.Create("posts-service-search");
        data.Posts.Add(CreatePost("Audi", "A4", "quattro diesel", 10000, true, false));
        await data.SaveChangesAsync();

        var service = CreateService(data);
        var search = new SearchPostDTO
        {
            Car = new SearchCarInputModelDTO
            {
                TextSearchTerm = "bmw",
            },
        };

        await Assert.ThrowsAsync<Exception>(() => service.GetMatchingPostsAsync(search, 0));
    }

    [Fact]
    public async Task GetSinglePostViewModelByIdAsync_ReturnsNull_WhenPostIsNotPublicAndPublicOnlyIsTrue()
    {
        await using var data = TestDbContextFactory.Create("posts-service-single");
        var post = CreatePost("Audi", "A4", "quattro diesel", 10000, false, false);
        data.Posts.Add(post);
        await data.SaveChangesAsync();

        var service = CreateService(data);

        var result = await service.GetSinglePostViewModelByIdAsync(post.Id);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetSinglePostViewModelByIdAsync_ReturnsPost_WhenPublicOnlyIsFalse()
    {
        await using var data = TestDbContextFactory.Create("posts-service-single");
        var safetyType = new ExtraType { Name = "Safety" };
        var airbag = new Extra { Name = "Airbag", Type = safetyType, TypeId = 2 };
        data.Extras.Add(airbag);

        var post = CreatePost("Audi", "A4", "quattro diesel", 10000, false, false, extras: [airbag]);
        data.Posts.Add(post);
        await data.SaveChangesAsync();

        var service = CreateService(data);

        var result = await service.GetSinglePostViewModelByIdAsync(post.Id, false);

        Assert.Equal("Seller", result!.SellerName);
    }

    private static PostsService CreateService(AutoOglasiDbContext data)
    {
        return new PostsService(data, new StubCarsService(), new ImagesService(data));
    }

    private static Post CreatePost(
        string make,
        string model,
        string description,
        decimal price,
        bool isPublic,
        bool isDeleted,
        IEnumerable<Extra>? extras = null)
    {
        var car = new Car
        {
            Make = make,
            Model = model,
            Description = description,
            Category = new Category { Name = $"{make}-category-{Guid.NewGuid()}" },
            FuelType = new FuelType { Name = $"{make}-fuel-{Guid.NewGuid()}" },
            TransmissionType = new TransmissionType { Name = $"{make}-transmission-{Guid.NewGuid()}" },
            Year = 2020,
            Kilometers = 100000,
            Horsepower = 150,
            Price = price,
            LocationCountry = "Serbia",
            LocationCity = "Belgrade",
        };

        car.Images.Add(new Image
        {
            CreatorId = "seller-1",
            Extension = "jpg",
            IsCoverImage = true,
        });

        foreach (var extra in extras ?? Enumerable.Empty<Extra>())
        {
            car.CarExtras.Add(new CarExtra
            {
                Car = car,
                Extra = extra,
            });
        }

        return new Post
        {
            Car = car,
            CreatorId = "seller-1",
            PublishedOn = new DateTime(2026, 4, 20),
            SellerName = "Seller",
            SellerPhoneNumber = "123456",
            IsPublic = isPublic,
            IsDeleted = isDeleted,
        };
    }

    private sealed class StubCarsService : ICarsService
    {
        public Task DeleteCarByIdAsync(int carId)
        {
            throw new NotSupportedException();
        }

        public Task FillInputCarBasePropertiesAsync(BaseCarInputModelDTO inputCar)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<CarExtrasServiceModel>> GetAllCarExtrasAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<BaseCarSpecificationServiceModel>> GetAllCategoriesAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<BaseCarSpecificationServiceModel>> GetAllFuelTypesAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<BaseCarSpecificationServiceModel>> GetAllTransmissionTypesAsync()
        {
            throw new NotSupportedException();
        }

        public Task<Car> GetCarFromInputModelAsync(CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, string userId, string imagePath)
        {
            throw new NotSupportedException();
        }

        public Task UpdateCarDataFromInputModelAsync(int carId, CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, List<string> deletedImagesIds, string userId, string imagePath, string coverImageId)
        {
            throw new NotSupportedException();
        }
    }
}
