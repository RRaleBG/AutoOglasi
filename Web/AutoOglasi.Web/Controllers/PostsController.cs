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
        public IActionResult Create()
        {
            var createPostServiceModel = new PostFormInputModelDTO();
            var createCarServiceModel = new CarFormInputModelDTO();

            this.carsService.FillInputCarBaseProperties(createCarServiceModel);

            createPostServiceModel.Car = createCarServiceModel;

            var createPostViewModel = mapper.Map<PostFormInputModel>(createPostServiceModel);
            var createCarViewModel = mapper.Map<CarFormInputModel>(createCarServiceModel);

            createPostViewModel.Car = createCarViewModel;

            return this.View(createPostViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostFormInputModel input)
        {
            var isAdmin = User.IsInRole("Administrator");

            var inputCarDTO = mapper.Map<CarFormInputModelDTO>(input.Car);

            if (!ModelState.IsValid)
            {
                carsService.FillInputCarBaseProperties(inputCarDTO);
                input.Car = mapper.Map<CarFormInputModel>(inputCarDTO);
                return View(input);
            }

            var userId = GetCurrentUserId();
            var inputPostDTO = mapper.Map<PostFormInputModelDTO>(input);
            var selectedExtrasIds = input.SelectedExtrasIds.ToList();
            var imagePath = $"{environment.WebRootPath}/images";
            int postId;

            try
            {
                var car = await carsService.GetCarFromInputModelAsync(inputCarDTO, selectedExtrasIds, userId, imagePath);
                postId = await postsService.CreateAsync(inputPostDTO, car, userId, isAdmin);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);

                carsService.FillInputCarBaseProperties(inputCarDTO);

                input.Car = mapper.Map<CarFormInputModel>(inputCarDTO);

                return View(input);
            }

            TempData[WebConstants.SuccessMessageKey] = $"The car post was added successfully{(isAdmin ? string.Empty : " and is awaiting for approval")}!";

            return RedirectToAction("Offer", new { Id = postId });
        }


        public IActionResult Search()
        {
            var searchCarDTO = new SearchCarInputModelDTO();
            var searchPostDTO = new SearchPostDTO();

            this.carsService.FillInputCarBaseProperties(searchCarDTO);

            searchPostDTO.Car = searchCarDTO;

            var searchCarInputModel = this.mapper.Map<SearchCarInputModel>(searchCarDTO);
            var searchPostInputModel = this.mapper.Map<SearchPostInputModel>(searchPostDTO);

            searchPostInputModel.Car = searchCarInputModel;

            return this.View(searchPostInputModel);
        }

        public IActionResult All(SearchPostInputModel searchPostInputModel, int id = 1, int sorting = 0)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }

                var searchPostDTO = mapper.Map<SearchPostDTO>(searchPostInputModel);
                var matchingPosts = postsService.GetMatchingPosts(searchPostDTO, sorting).ToList();
                var postsByPageDTOs = postsService.GetPostsByPage(matchingPosts, id, PostsPerPage);
                var postsByPageAsViewModels = mapper.Map<IEnumerable<PostInListDTO>, IEnumerable<PostInListViewModel>>(postsByPageDTOs);

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
                var searchCarInputModelDTO = mapper.Map<SearchCarInputModelDTO>(searchCarInputModel);

                carsService.FillInputCarBaseProperties(searchCarInputModelDTO);

                searchCarInputModel = mapper.Map<SearchCarInputModel>(searchCarInputModelDTO);
                searchPostInputModel.Car = searchCarInputModel;

                return View("Search", searchPostInputModel);
            }
        }

        public IActionResult Offer(int id)
        {
            var isAdmin = User.IsInRole("Administrator");
            var publicOnly = !isAdmin;

            if (User.Identity.IsAuthenticated && !isAdmin)
            {
                var userId = GetCurrentUserId();
                var postCreatorId = postsService.GetPostCreatorId(id);

                publicOnly = userId != postCreatorId;
            }

            var singlePostDataDTO = postsService.GetSinglePostViewModelById(id, publicOnly);

            if (singlePostDataDTO == null)
            {
                return NotFound();
            }

            var singlePostViewModel = mapper.Map<SinglePostViewModel>(singlePostDataDTO);

            return View(singlePostViewModel);
        }


        [Authorize]
        public IActionResult Mine(int id = 1, int sorting = 0)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();
            var postsByUserDTOs = postsService.GetPostsByUser(userId, sorting).ToList();
            var postsByUserDTOsForThisPage = postsService.GetPostsByPage(postsByUserDTOs, id, PostsPerPage);
            var postsByUserViewModels = mapper.Map<IEnumerable<PostByUserDTO>, IEnumerable<PostByUserViewModel>>(postsByUserDTOsForThisPage);

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
        public IActionResult Edit(int id)
        {
            var userId = GetCurrentUserId();
            var editPostDTO = postsService.GetPostFormInputModelById(id);

            if (editPostDTO == null)
            {
                return NotFound();
            }

            if ((editPostDTO.CreatorId != userId) && !User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            carsService.FillInputCarBaseProperties(editPostDTO.Car);

            var editPostViewModel = mapper.Map<EditPostViewModel>(editPostDTO);

            return View(editPostViewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditPostViewModel editedPost)
        {
            var userId = GetCurrentUserId();
            var isAdmin = User.IsInRole(AdministratorRoleName);

            if ((editedPost.CreatorId != userId) && !User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            var editedPostDTO = mapper.Map<EditPostDTO>(editedPost);
            var currentImagesDTO = postsService.GetCurrentDbImagesForAPost(id);
            var currentImagesViewModel = mapper.Map<IEnumerable<ImageInfoDTO>, IEnumerable<ImageInfoViewModel>>(currentImagesDTO);

            if (!ModelState.IsValid)
            {
                carsService.FillInputCarBaseProperties(editedPostDTO.Car);

                var editedPostViewModel = mapper.Map<EditPostViewModel>(editedPostDTO);
                editedPostViewModel.CurrentImages = currentImagesViewModel;

                return this.View(editedPostViewModel);
            }

            var selectedExtrasIds = editedPost.SelectedExtrasIds.ToList();
            var deletedImagesIds = editedPost.DeletedImagesIds.ToList();
            var imagePath = $"{environment.WebRootPath}/images";

            try
            {
                await carsService.UpdateCarDataFromInputModelAsync(editedPostDTO.CarId, editedPostDTO.Car, selectedExtrasIds, deletedImagesIds, userId, imagePath, editedPost.SelectedCoverImageId);
                await postsService.UpdateAsync(id, editedPostDTO, isAdmin);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", ex.Message);
                carsService.FillInputCarBaseProperties(editedPostDTO.Car);

                editedPost = mapper.Map<EditPostViewModel>(editedPostDTO);
                editedPost.CurrentImages = currentImagesViewModel;

                return View(editedPost);
            }

            TempData[WebConstants.SuccessMessageKey] = $"The car post was edited successfully{(isAdmin ? string.Empty : " and is awaiting for approval!")}!";

            return RedirectToAction("Offer", new { Id = id });
        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = GetCurrentUserId();

            var postDTO = postsService.GetBasicPostInformationById(id);

            if (postDTO == null)
            {
                return NotFound();
            }

            var postCreatorId = postsService.GetPostCreatorId(id);

            if ((userId != postCreatorId) && !User.IsInRole(AdministratorRoleName))
            {
                return Unauthorized();
            }

            var postByUserViewModel = mapper.Map<PostByUserViewModel>(postDTO);

            return View(postByUserViewModel);
        }


        [HttpPost]
        [Authorize]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetCurrentUserId();

            var postCreatorId = postsService.GetPostCreatorId(id);

            var isAdmin = User.IsInRole(AdministratorRoleName);

            if ((userId != postCreatorId) && !isAdmin)
            {
                return Unauthorized();
            }

            try
            {
                await postsService.DeletePostByIdAsync(id);
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