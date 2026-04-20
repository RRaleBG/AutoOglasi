namespace AutoOglasi.Services.Tests;

using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Posts.Models;
using AutoOglasi.Web.ViewModels.Posts;
using AdminPostsController = AutoOglasi.Web.Areas.Admin.Controllers.PostsController;
using Microsoft.AspNetCore.Mvc;

public class AdminPostsControllerTests
{
    [Fact]
    public async Task All_ReturnsNotFound_WhenPageIsInvalid()
    {
        var controller = new AdminPostsController(new StubPostsService(), TestMapperFactory.CreateControllerMapper());

        var result = await controller.All(0);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task All_ReturnsViewModel_WhenPageIsValid()
    {
        var postsService = new StubPostsService
        {
            PostsToReturn =
            [
                new BasePostInListDTO
                {
                    Id = 3,
                    PublishedOn = "ponedeljak 20.apr",
                    IsPublic = true,
                    Car = new AutoOglasi.Services.Cars.Models.BaseCarDTO
                    {
                        Id = 5,
                        Make = "Audi",
                        Model = "A4",
                        Year = 2020,
                        Price = 10000,
                    },
                },
            ],
            PostsCountToReturn = 1,
        };

        var controller = new AdminPostsController(postsService, TestMapperFactory.CreateControllerMapper());

        var result = await controller.All(1);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<PostsListAdminAreaViewModel>(viewResult.Model);
    }

    [Fact]
    public async Task ChangeVisibility_RedirectsToAll()
    {
        var postsService = new StubPostsService();
        var controller = new AdminPostsController(postsService, TestMapperFactory.CreateControllerMapper());

        var result = await controller.ChangeVisibility(4);

        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("All", redirectResult.ActionName);
        Assert.Equal(4, postsService.ChangedVisibilityPostId);
    }

    private sealed class StubPostsService : IPostsService
    {
        public IEnumerable<BasePostInListDTO> PostsToReturn { get; init; } = Enumerable.Empty<BasePostInListDTO>();

        public int PostsCountToReturn { get; init; }

        public int? ChangedVisibilityPostId { get; private set; }

        public Task ChangeVisibilityAsync(int postId)
        {
            this.ChangedVisibilityPostId = postId;
            return Task.CompletedTask;
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
            return Task.FromResult(this.PostsCountToReturn);
        }

        public Task<IEnumerable<BasePostInListDTO>> GetAllPostsBaseInfoAsync(int page, int postsPerPage)
        {
            return Task.FromResult(this.PostsToReturn);
        }

        public Task<PostByUserDTO> GetBasicPostInformationByIdAsync(int postId)
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<AutoOglasi.Services.Images.Models.ImageInfoDTO>> GetCurrentDbImagesForAPostAsync(int postId)
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
