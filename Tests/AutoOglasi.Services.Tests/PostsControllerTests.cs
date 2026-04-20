namespace AutoOglasi.Services.Tests;

using System.Security.Claims;
using AutoMapper;
using AutoOglasi.Data.Models;
using AutoOglasi.Services.Cars;
using AutoOglasi.Services.Cars.Models;
using AutoOglasi.Services.Images.Models;
using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Posts.Models;
using AutoOglasi.Web.Controllers;
using AutoOglasi.Web.ViewModels.Posts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.FileProviders;

public class PostsControllerTests
{
    [Fact]
    public async Task All_ReturnsNotFound_WhenPageIsInvalid()
    {
        var controller = CreateController();

        var result = await controller.All(new SearchPostInputModel(), 0);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Mine_ReturnsNotFound_WhenPageIsInvalid()
    {
        var controller = CreateController();

        var result = await controller.Mine(0);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Offer_ReturnsView_WhenPostExists()
    {
        var postsService = new StubPostsService
        {
            SinglePostToReturn = new SinglePostDTO
            {
                PublishedOn = "ponedeljak 20.apr",
                SellerName = "Seller",
                SellerPhoneNumber = "123456",
                Car = new SingleCarDTO
                {
                    Id = 5,
                    Make = "Audi",
                    Model = "A4",
                    Year = 2020,
                    Price = 10000,
                    Description = "Maintained car",
                    Kilometers = 150000,
                    Horsepower = 150,
                    Category = "Sedan",
                    FuelType = "Diesel",
                    TransmissionType = "Manual",
                    Images = new List<string> { "/images/cars/audi.jpg" },
                    ComfortExtras = new List<string>(),
                    SafetyExtras = new List<string>(),
                    OtherExtras = new List<string>(),
                    LocationCountry = "Serbia",
                    LocationCity = "Belgrade",
                },
            },
        };

        var controller = CreateController(postsService: postsService);

        var result = await controller.Offer(5);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<SinglePostViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task Offer_ReturnsNotFound_WhenPostDoesNotExist()
    {
        var controller = CreateController(postsService: new StubPostsService());

        var result = await controller.Offer(5);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsUnauthorized_WhenUserIsNotOwner()
    {
        var postsService = new StubPostsService
        {
            BasicPostToReturn = new PostByUserDTO
            {
                Id = 4,
                PublishedOn = "ponedeljak 20.apr",
                Car = new CarByUserDTO
                {
                    Id = 8,
                    Make = "Audi",
                    Model = "A4",
                    Year = 2020,
                    Price = 10000,
                    CoverImage = "/images/cars/audi.jpg",
                },
            },
            CreatorIdToReturn = "owner-2",
        };

        var controller = CreateController(
            postsService: postsService,
            userId: "owner-1");

        var result = await controller.Delete(4);

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task DeleteConfirmed_RedirectsToAdminPosts_WhenUserIsAdministrator()
    {
        var postsService = new StubPostsService
        {
            CreatorIdToReturn = "owner-2",
        };

        var controller = CreateController(
            postsService: postsService,
            userId: "admin-1",
            isAdministrator: true);

        var result = await controller.DeleteConfirmed(7);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("All", redirectResult.ActionName);
        Assert.Equal("Posts", redirectResult.ControllerName);
        Assert.True(postsService.DeleteWasCalled);
    }

    private static PostsController CreateController(
        StubCarsService? carsService = null,
        StubPostsService? postsService = null,
        string? userId = null,
        bool isAdministrator = false)
    {
        var controller = new PostsController(
            carsService ?? new StubCarsService(),
            postsService ?? new StubPostsService(),
            new TestWebHostEnvironment(),
            TestMapperFactory.CreateControllerMapper());

        var claims = new List<Claim>();
        if (userId != null)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
        }

        if (isAdministrator)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
        }

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = claims.Count == 0
                    ? new ClaimsPrincipal(new ClaimsIdentity())
                    : new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth")),
            },
        };

        controller.TempData = new TempDataDictionary(controller.HttpContext, new TestTempDataProvider());
        return controller;
    }

    private sealed class StubCarsService : ICarsService
    {
        public Task DeleteCarByIdAsync(int carId)
        {
            throw new NotSupportedException();
        }

        public Task FillInputCarBasePropertiesAsync(BaseCarInputModelDTO inputCar)
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<CarExtrasServiceModel>> GetAllCarExtrasAsync()
        {
            return Task.FromResult(Enumerable.Empty<CarExtrasServiceModel>());
        }

        public Task<IEnumerable<BaseCarSpecificationServiceModel>> GetAllCategoriesAsync()
        {
            return Task.FromResult(Enumerable.Empty<BaseCarSpecificationServiceModel>());
        }

        public Task<IEnumerable<BaseCarSpecificationServiceModel>> GetAllFuelTypesAsync()
        {
            return Task.FromResult(Enumerable.Empty<BaseCarSpecificationServiceModel>());
        }

        public Task<IEnumerable<BaseCarSpecificationServiceModel>> GetAllTransmissionTypesAsync()
        {
            return Task.FromResult(Enumerable.Empty<BaseCarSpecificationServiceModel>());
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

    private sealed class StubPostsService : IPostsService
    {
        public SinglePostDTO? SinglePostToReturn { get; init; }

        public PostByUserDTO? BasicPostToReturn { get; init; }

        public string? CreatorIdToReturn { get; init; }

        public bool DeleteWasCalled { get; private set; }

        public Task ChangeVisibilityAsync(int postId)
        {
            throw new NotSupportedException();
        }

        public Task<int> CreateAsync(PostFormInputModelDTO inputPost, Car car, string userId, bool isPublic)
        {
            throw new NotSupportedException();
        }

        public Task DeletePostByIdAsync(int postId)
        {
            this.DeleteWasCalled = true;
            return Task.CompletedTask;
        }

        public Task<int> GetAllPostsCountAsync()
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<BasePostInListDTO>> GetAllPostsBaseInfoAsync(int page, int postsPerPage)
        {
            throw new NotSupportedException();
        }

        public Task<PostByUserDTO> GetBasicPostInformationByIdAsync(int postId)
        {
            return Task.FromResult(this.BasicPostToReturn)!;
        }

        public Task<IEnumerable<ImageInfoDTO>> GetCurrentDbImagesForAPostAsync(int postId)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<PostInLatestListDTO>> GetLatestAsync(int count)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<PostInListDTO>> GetMatchingPostsAsync(SearchPostDTO searchInputModel, int sortingNumber)
        {
            throw new NotSupportedException();
        }

        public Task<EditPostDTO> GetPostFormInputModelByIdAsync(int postId)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<T> GetPostsByPage<T>(IEnumerable<T> posts, int page, int postsPerPage)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<PostByUserDTO>> GetPostsByUserAsync(string userId, int sortingNumber)
        {
            throw new NotSupportedException();
        }

        public Task<string> GetPostCreatorIdAsync(int postId)
        {
            return Task.FromResult(this.CreatorIdToReturn)!;
        }

        public Task<SinglePostDTO> GetSinglePostViewModelByIdAsync(int postId, bool publicOnly = true)
        {
            return Task.FromResult(this.SinglePostToReturn)!;
        }

        public Task UpdateAsync(int postId, EditPostDTO input, bool isPublic)
        {
            throw new NotSupportedException();
        }
    }

    private sealed class TestWebHostEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "AutoOglasi.Tests";

        public IFileProvider WebRootFileProvider { get; set; } = null!;

        public string WebRootPath { get; set; } = "wwwroot";

        public string EnvironmentName { get; set; } = "Development";

        public string ContentRootPath { get; set; } = "content";

        public IFileProvider ContentRootFileProvider { get; set; } = null!;
    }
}
