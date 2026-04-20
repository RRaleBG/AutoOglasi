namespace AutoOglasi.Web.Controllers
{
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;
    using ViewModels;
    using ViewModels.Posts;
    using Services.Posts;
    using Services.Posts.Models;

    public class HomeController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IMapper mapper;

        public HomeController(IPostsService postsService, IMapper mapper)
        {
            this.postsService = postsService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.BuildLatestPostsViewModelAsync());
        }

        public async Task<IActionResult> About()
        {
            return View(await this.BuildLatestPostsViewModelAsync());
        }

        public IActionResult Error(string code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = code });
        }

        public IActionResult StatusCode(string code)
        {
            return RedirectToAction("Error", new { code });
        }

        private async Task<LatestPostsViewModel> BuildLatestPostsViewModelAsync()
        {
            var latestPostsDto = await this.postsService.GetLatestAsync(8);
            var latestPostsViewModel = this.mapper.Map<IEnumerable<PostInLatestListDTO>, IEnumerable<PostInLatestListViewModel>>(latestPostsDto);

            return new LatestPostsViewModel
            {
                LatestPosts = latestPostsViewModel,
            };
        }
    }
}
