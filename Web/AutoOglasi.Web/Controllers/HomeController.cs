namespace AutoOglasi.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.Posts;
    using Services.Posts.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using ViewModels;
    using ViewModels.Posts;

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptCookies()
        {
            Response.Cookies.Append("Consent", "true", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true
            });

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Ok();

            return RedirectToAction("Index", "Home");
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
