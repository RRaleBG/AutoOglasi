namespace AutoOglasi.Web.Areas.Admin.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Posts;
    using Services.Posts.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ViewModels.Posts;
    using static AdminConstants;

    //AdminAreaName
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PostsController : Controller
    {
        private const int PostsPerPage = 10;

        private readonly IPostsService postsService;
        private readonly IMapper mapper;



        public PostsController(IPostsService postsService, IMapper mapper)
        {
            this.postsService = postsService;
            this.mapper = mapper;
        }



        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var postsDTO = postsService.GetAllPostsBaseInfo(id, 10);
            var postsViewModel = mapper.Map<IEnumerable<BasePostInListDTO>, IEnumerable<PostInAdminAreaViewModel>>(postsDTO);
            var allPosts = postsService.GetAllPostsCount();

            var postsListViewModel = new PostsListAdminAreaViewModel()
            {
                PageNumber = id,
                PostsPerPage = PostsPerPage,
                PostsCount = allPosts,
                Posts = postsViewModel,
            };

            if (id > postsListViewModel.PagesCount)
            {
                return View(postsListViewModel);
            }

            return View(postsListViewModel);
        }



        public async Task<IActionResult> ChangeVisibility(int id)
        {
            await postsService.ChangeVisibilityAsync(id);

            return RedirectToAction("All");
        }
    }
}