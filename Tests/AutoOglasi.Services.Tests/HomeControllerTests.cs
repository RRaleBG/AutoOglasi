namespace AutoOglasi.Services.Tests;

using AutoMapper;
using AutoOglasi.Services.Cars.Models;
using AutoOglasi.Services.Images.Models;
using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Posts.Models;
using AutoOglasi.Web.Controllers;
using AutoOglasi.Web.ViewModels;
using AutoOglasi.Web.ViewModels.Cars;
using AutoOglasi.Web.ViewModels.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

public class HomeControllerTests
{
    [Fact]
    public async Task Index_ReturnsLatestPostsViewModel()
    {
        var controller = CreateController(CreateLatestPosts());

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<LatestPostsViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task About_ReturnsLatestPostsViewModel()
    {
        var controller = CreateController(CreateLatestPosts());

        var result = await controller.About();

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<LatestPostsViewModel>(viewResult.Model);
    }

    [Fact]
    public void Error_ReturnsErrorViewModelWithProvidedStatusCode()
    {
        var controller = CreateController(Array.Empty<PostInLatestListDTO>());
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { TraceIdentifier = "trace-1" },
        };

        var result = controller.Error("404");

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
        Assert.Equal("404", model.StatusCode);
    }

    [Fact]
    public void StatusCode_RedirectsToErrorAction()
    {
        var controller = CreateController(Array.Empty<PostInLatestListDTO>());

        var result = controller.StatusCode("500");

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Error", redirectResult.ActionName);
    }

    private static HomeController CreateController(IEnumerable<PostInLatestListDTO> latestPosts)
    {
        return new HomeController(new StubPostsService(latestPosts), CreateMapper());
    }

    private static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BaseCarDTO, BaseCarViewModel>();
                cfg.CreateMap<LatestPostsCarDTO, LatestPostsCarViewModel>();
                cfg.CreateMap<PostInLatestListDTO, PostInLatestListViewModel>();
            },
            NullLoggerFactory.Instance);

        return configuration.CreateMapper();
    }

    private static IEnumerable<PostInLatestListDTO> CreateLatestPosts()
    {
        return
        [
            new PostInLatestListDTO
            {
                Id = 1,
                PublishedOn = "2026-04-20",
                Car = new LatestPostsCarDTO
                {
                    Id = 2,
                    Make = "Audi",
                    Model = "A4",
                    Year = 2020,
                    Price = 10000,
                    Horsepower = 150,
                    FuelType = "Diesel",
                    TransmissionType = "Manual",
                    CoverImage = "/images/cars/audi.jpg",
                },
            },
        ];
    }

    private sealed class StubPostsService : IPostsService
    {
        private readonly IEnumerable<PostInLatestListDTO> latestPosts;

        public StubPostsService(IEnumerable<PostInLatestListDTO> latestPosts)
        {
            this.latestPosts = latestPosts;
        }

        public Task ChangeVisibilityAsync(int postId)
        {
            throw new NotSupportedException();
        }

        public Task<int> CreateAsync(PostFormInputModelDTO inputPost, AutoOglasi.Data.Models.Car car, string userId, bool isPublic)
        {
            throw new NotSupportedException();
        }

        public Task DeletePostByIdAsync(int postId)
        {
            throw new NotSupportedException();
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
            throw new NotSupportedException();
        }

        public Task<IEnumerable<ImageInfoDTO>> GetCurrentDbImagesForAPostAsync(int postId)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<PostInLatestListDTO>> GetLatestAsync(int count)
        {
            return Task.FromResult(this.latestPosts);
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
            throw new NotSupportedException();
        }

        public Task<SinglePostDTO> GetSinglePostViewModelByIdAsync(int postId, bool publicOnly = true)
        {
            throw new NotSupportedException();
        }

        public Task UpdateAsync(int postId, EditPostDTO input, bool isPublic)
        {
            throw new NotSupportedException();
        }
    }
}
