namespace AutoOglasi.Services.Tests;

using AutoOglasi.Data;
using AutoOglasi.Data.Models;
using AutoOglasi.Services.Cars;
using AutoOglasi.Services.Images;
using AutoOglasi.Services.Images.Models;
using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Posts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging.Abstractions;

public class PostsServiceTests
{
    [Fact]
    public async Task CreateAsync_SavesPostAndReturnsGeneratedId()
    {
        await using var data = TestDbContextFactory.Create("posts-service-tests");
        var service = CreateService(data);
        var input = new PostFormInputModelDTO
        {
            SellerName = "Seller One",
            SellerPhoneNumber = "123456",
        };
        var car = new Car
        {
            Make = "Audi",
            Model = "A4",
            Description = "Clean car",
            CategoryId = 1,
            FuelTypeId = 1,
            TransmissionTypeId = 1,
            Year = 2020,
            Kilometers = 100000,
            Horsepower = 150,
            Price = 10000,
            LocationCountry = "Serbia",
            LocationCity = "Belgrade",
        };

        var result = await service.CreateAsync(input, car, "user-1", true);

        Assert.True(result > 0);
    }

    [Fact]
    public async Task GetAllPostsCountAsync_ReturnsStoredCount()
    {
        await using var data = TestDbContextFactory.Create("posts-service-tests");
        data.Posts.AddRange(
            new Post { CreatorId = "user-1", CarId = 1, SellerName = "One", SellerPhoneNumber = "111111" },
            new Post { CreatorId = "user-2", CarId = 2, SellerName = "Two", SellerPhoneNumber = "222222" });
        await data.SaveChangesAsync();
        var service = CreateService(data);

        var result = await service.GetAllPostsCountAsync();

        Assert.Equal(2, result);
    }

    [Fact]
    public void GetPostsByPage_ReturnsRequestedSlice()
    {
        using var data = TestDbContextFactory.Create("posts-service-tests");
        var service = CreateService(data);
        var posts = Enumerable.Range(1, 10).ToList();

        var result = service.GetPostsByPage(posts, 2, 3).ToList();

        Assert.Equal(4, result[0]);
    }


    [Fact]
    public async Task NotifyUsersAboutPublishedPostAsync_SendsThemedEmailToConfirmedUsers()
    {
        await using var data = TestDbContextFactory.Create("posts-service-tests-notify");
        var confirmedUser = new ApplicationUser
        {
            Id = "user-1",
            Email = "confirmed@example.com",
            EmailConfirmed = true,
            UserName = "confirmed@example.com",
        };
        var unconfirmedUser = new ApplicationUser
        {
            Id = "user-2",
            Email = "unconfirmed@example.com",
            EmailConfirmed = false,
            UserName = "unconfirmed@example.com",
        };
        data.Users.AddRange(confirmedUser, unconfirmedUser);

        var post = new Post
        {
            Car = new Car
            {
                Make = "Audi",
                Model = "A4",
                Category = new Category { Name = "Category" },
                FuelType = new FuelType { Name = "Fuel" },
                TransmissionType = new TransmissionType { Name = "Transmission" },
                Year = 2020,
                Kilometers = 100000,
                Horsepower = 150,
                Price = 10000,
                LocationCountry = "Serbia",
                LocationCity = "Belgrade",
            },
            CreatorId = confirmedUser.Id,
            SellerName = "Seller One",
            SellerPhoneNumber = "123456",
            IsPublic = true,
            IsDeleted = false,
        };
        data.Posts.Add(post);
        await data.SaveChangesAsync();

        var emailSender = new StubEmailSender();
        var service = CreateService(data, emailSender);

        await service.NotifyUsersAboutPublishedPostAsync(post.Id, "https://example.com/posts/1");

        Assert.Single(emailSender.SentEmails);
        Assert.Equal("confirmed@example.com", emailSender.SentEmails[0].To);
        Assert.Equal("New listing published: Audi A4", emailSender.SentEmails[0].Subject);
        Assert.Contains("https://example.com/posts/1", emailSender.SentEmails[0].Body);
        Assert.DoesNotContain("unconfirmed@example.com", emailSender.SentEmails[0].Body);
    }

    private static PostsService CreateService(AutoOglasiDbContext data, IEmailSender? emailSender = null)
    {
        return new PostsService(data, new StubCarsService(), new StubImagesService(), emailSender ?? new StubEmailSender(), NullLogger<PostsService>.Instance);
    }
    private sealed class StubCarsService : ICarsService
    {
        public Task DeleteCarByIdAsync(int carId)
        {
            throw new NotSupportedException();
        }

        public Task FillInputCarBasePropertiesAsync(AutoOglasi.Services.Cars.Models.BaseCarInputModelDTO inputCar)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<AutoOglasi.Services.Cars.Models.CarExtrasServiceModel>> GetAllCarExtrasAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<AutoOglasi.Services.Cars.Models.BaseCarSpecificationServiceModel>> GetAllCategoriesAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<AutoOglasi.Services.Cars.Models.BaseCarSpecificationServiceModel>> GetAllFuelTypesAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<AutoOglasi.Services.Cars.Models.BaseCarSpecificationServiceModel>> GetAllTransmissionTypesAsync()
        {
            throw new NotSupportedException();
        }

        public Task<Car> GetCarFromInputModelAsync(AutoOglasi.Services.Cars.Models.CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, string userId, string imagePath)
        {
            throw new NotSupportedException();
        }

        public Task UpdateCarDataFromInputModelAsync(int carId, AutoOglasi.Services.Cars.Models.CarFormInputModelDTO inputCar, List<int> selectedExtrasIds, List<string> deletedImagesIds, string userId, string imagePath, string coverImageId)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class StubImagesService : IImagesService
    {
        public string GetCoverImagePath(ICollection<Image> carImages)
        {
            return "/images/cars/cover.jpg";
        }

        public string GetDefaultCarImagesPath(string imageId, string imageExtension)
        {
            return $"/images/cars/{imageId}.{imageExtension}";
        }

        public Task RemoveCoverImagePropertyAsync(string imageId)
        {
            throw new NotSupportedException();
        }

        public Task SetCoverImagePropertyAsync(string imageId)
        {
            throw new NotSupportedException();
        }

        public Task<Image> UploadImageAsync(IFormFile image, string userId, string imagePath)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class StubEmailSender : IEmailSender
    {
        public List<SentEmail> SentEmails { get; } = new();

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            this.SentEmails.Add(new SentEmail(email, subject, htmlMessage));
            return Task.CompletedTask;
        }
    }

    private sealed record SentEmail(string To, string Subject, string Body);
}
