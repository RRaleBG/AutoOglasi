using AutoMapper;
using AutoOglasi.Services.Cars;
using AutoOglasi.Services.Cars.Models;
using AutoOglasi.Services.Images.Models;
using AutoOglasi.Services.Posts;
using AutoOglasi.Services.Posts.Models;
using AutoOglasi.Web.Constants;
using AutoOglasi.Web.ViewModels.Cars;
using AutoOglasi.Web.ViewModels.Images;
using AutoOglasi.Web.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static AutoOglasi.GlobalConstants.GlobalConstants;

namespace AutoOglasi.Web.Controllers
{
    public class PostsController : Controller
    {
        private const int PostsPerPage = 8;

        private readonly ICarsService carsService;
        private readonly IPostsService postsService;
        private readonly IWebHostEnvironment environment;
        private readonly IMapper mapper;

        public PostsController(ICarsService carsService, IPostsService postsService, IWebHostEnvironment environment, IMapper mapper)
        {
            this.carsService = carsService;
            this.postsService = postsService;
            this.environment = environment;
            this.mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var createPostServiceModel = new PostFormInputModelDTO();
            var createCarServiceModel = new CarFormInputModelDTO();

            await this.carsService.FillInputCarBasePropertiesAsync(createCarServiceModel);

            createPostServiceModel.Car = createCarServiceModel;

            var createPostViewModel = this.mapper.Map<PostFormInputModel>(createPostServiceModel);
            var createCarViewModel = this.mapper.Map<CarFormInputModel>(createCarServiceModel);

            createPostViewModel.Car = createCarViewModel;

            return this.View(createPostViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(PostFormInputModel input)
        {
            var isAdmin = User.IsInRole("Administrator");

            var inputCarDTO = this.mapper.Map<CarFormInputModelDTO>(input.Car);

            if (!ModelState.IsValid)
            {
                await this.carsService.FillInputCarBasePropertiesAsync(inputCarDTO);
                input.Car = this.mapper.Map<CarFormInputModel>(inputCarDTO);
                return this.View(input);
            }

            var userId = this.GetCurrentUserId();
            var inputPostDTO = this.mapper.Map<PostFormInputModelDTO>(input);
            var selectedExtrasIds = input.SelectedExtrasIds.ToList();
            var imagePath = $"{this.environment.WebRootPath}/images";
            int postId;

            try
            {
                var car = await this.carsService.GetCarFromInputModelAsync(inputCarDTO, selectedExtrasIds, userId, imagePath);
                postId = await this.postsService.CreateAsync(inputPostDTO, car, userId, isAdmin);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);

                await this.carsService.FillInputCarBasePropertiesAsync(inputCarDTO);

                input.Car = this.mapper.Map<CarFormInputModel>(inputCarDTO);

                return this.View(input);
            }

            TempData[WebConstants.SuccessMessageKey] = $"The car post was added successfully{(isAdmin ? string.Empty : " and is awaiting for approval")}!";

            return RedirectToAction("Offer", new { Id = postId });
        }

        public async Task<IActionResult> Search()
        {
            var searchCarDTO = new SearchCarInputModelDTO();
            var searchPostDTO = new SearchPostDTO();

            await this.carsService.FillInputCarBasePropertiesAsync(searchCarDTO);

            searchPostDTO.Car = searchCarDTO;

            var searchCarInputModel = this.mapper.Map<SearchCarInputModel>(searchCarDTO);
            var searchPostInputModel = this.mapper.Map<SearchPostInputModel>(searchPostDTO);

            searchPostInputModel.Car = searchCarInputModel;

            return this.View(searchPostInputModel);
        }

        public async Task<IActionResult> All(SearchPostInputModel searchPostInputModel, int id = 1, int sorting = 0)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }

                var searchPostDTO = this.mapper.Map<SearchPostDTO>(searchPostInputModel);
                var matchingPosts = (await this.postsService.GetMatchingPostsAsync(searchPostDTO, sorting)).ToList();
                var postsByPageDTOs = this.postsService.GetPostsByPage(matchingPosts, id, PostsPerPage);
                var postsByPageAsViewModels = this.mapper.Map<IEnumerable<PostInListDTO>, IEnumerable<PostInListViewModel>>(postsByPageDTOs);

                var postsListViewModel = new PostsListViewModel()
                {
                    PageNumber = id,
                    PostsPerPage = PostsPerPage,
                    PostsCount = matchingPosts.Count,
                    Posts = postsByPageAsViewModels,
                };

                if (id > postsListViewModel.PagesCount)
                {
                    return NotFound();
                }

                return View(postsListViewModel);
            }
            catch (Exception ex)
            {
                TempData[WebConstants.ErrorMessageKey] = ex.Message;

                var searchCarInputModel = searchPostInputModel.Car;
                var searchCarInputModelDTO = this.mapper.Map<SearchCarInputModelDTO>(searchCarInputModel);

                await this.carsService.FillInputCarBasePropertiesAsync(searchCarInputModelDTO);

                searchCarInputModel = this.mapper.Map<SearchCarInputModel>(searchCarInputModelDTO);
                searchPostInputModel.Car = searchCarInputModel;

                return View("Search", searchPostInputModel);
            }
        }

        public async Task<IActionResult> Offer(int id)
        {
            var isAdmin = User.IsInRole("Administrator");
            var publicOnly = !isAdmin;

            if (User.Identity.IsAuthenticated && !isAdmin)
            {
                var userId = this.GetCurrentUserId();
                var postCreatorId = await this.postsService.GetPostCreatorIdAsync(id);

                publicOnly = userId != postCreatorId;
            }

            var singlePostDataDTO = await this.postsService.GetSinglePostViewModelByIdAsync(id, publicOnly);

            if (singlePostDataDTO == null)
            {
                return NotFound();
            }

            var singlePostViewModel = this.mapper.Map<SinglePostViewModel>(singlePostDataDTO);

            return View(singlePostViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Mine(int id = 1, int sorting = 0)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var userId = this.GetCurrentUserId();
            var postsByUserDTOs = (await this.postsService.GetPostsByUserAsync(userId, sorting)).ToList();
            var postsByUserDTOsForThisPage = this.postsService.GetPostsByPage(postsByUserDTOs, id, PostsPerPage);
            var postsByUserViewModels = this.mapper.Map<IEnumerable<PostByUserDTO>, IEnumerable<PostByUserViewModel>>(postsByUserDTOsForThisPage);

            var postsByUserViewModel = new PostsByUserViewModel()
            {
                PageNumber = id,
                PostsPerPage = PostsPerPage,
                PostsCount = postsByUserDTOs.Count,
                Posts = postsByUserViewModels,
            };

            if (id > postsByUserViewModel.PagesCount && id > 1)
            {
                return NotFound();
            }

            return View(postsByUserViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.GetCurrentUserId();
            var editPostDTO = await this.postsService.GetPostFormInputModelByIdAsync(id);

            if (editPostDTO == null)
            {
                return NotFound();
            }

            if ((editPostDTO.CreatorId != userId) && !User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            await this.carsService.FillInputCarBasePropertiesAsync(editPostDTO.Car);

            var editPostViewModel = this.mapper.Map<EditPostViewModel>(editPostDTO);

            return View(editPostViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPostViewModel editedPost)
        {
            var userId = this.GetCurrentUserId();
            var isAdmin = User.IsInRole(AdministratorRoleName);

            if ((editedPost.CreatorId != userId) && !User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            var editedPostDTO = this.mapper.Map<EditPostDTO>(editedPost);
            var currentImagesDTO = await this.postsService.GetCurrentDbImagesForAPostAsync(id);
            var currentImagesViewModel = this.mapper.Map<IEnumerable<ImageInfoDTO>, IEnumerable<ImageInfoViewModel>>(currentImagesDTO);

            if (!ModelState.IsValid)
            {
                await this.carsService.FillInputCarBasePropertiesAsync(editedPostDTO.Car);

                var editedPostViewModel = this.mapper.Map<EditPostViewModel>(editedPostDTO);
                editedPostViewModel.CurrentImages = currentImagesViewModel;

                return this.View(editedPostViewModel);
            }

            var selectedExtrasIds = editedPost.SelectedExtrasIds.ToList();
            var deletedImagesIds = editedPost.DeletedImagesIds.ToList();
            var imagePath = $"{this.environment.WebRootPath}/images";

            try
            {
                await this.carsService.UpdateCarDataFromInputModelAsync(editedPostDTO.CarId, editedPostDTO.Car, selectedExtrasIds, deletedImagesIds, userId, imagePath, editedPost.SelectedCoverImageId);
                await this.postsService.UpdateAsync(id, editedPostDTO, isAdmin);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                await this.carsService.FillInputCarBasePropertiesAsync(editedPostDTO.Car);

                editedPost = this.mapper.Map<EditPostViewModel>(editedPostDTO);
                editedPost.CurrentImages = currentImagesViewModel;

                return View(editedPost);
            }

            TempData[WebConstants.SuccessMessageKey] = $"The car post was edited successfully{(isAdmin ? string.Empty : " and is awaiting for approval!")}!";

            return RedirectToAction("Offer", new { Id = id });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.GetCurrentUserId();
            var postDTO = await this.postsService.GetBasicPostInformationByIdAsync(id);

            if (postDTO == null)
            {
                return NotFound();
            }

            var postCreatorId = await this.postsService.GetPostCreatorIdAsync(id);

            if ((userId != postCreatorId) && !User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            var postByUserViewModel = this.mapper.Map<PostByUserViewModel>(postDTO);

            return View(postByUserViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = this.GetCurrentUserId();
            var postCreatorId = await this.postsService.GetPostCreatorIdAsync(id);
            var isAdmin = User.IsInRole(AdministratorRoleName);

            if ((userId != postCreatorId) && !isAdmin)
            {
                return Unauthorized();
            }

            try
            {
                await this.postsService.DeletePostByIdAsync(id);
                TempData[WebConstants.SuccessMessageKey] = "The car post was deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData[WebConstants.ErrorMessageKey] = ex.Message;
            }

            if (isAdmin)
            {
                return RedirectToAction("All", "Posts", new { area = "Admin" });
            }

            return RedirectToAction("Mine");
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}