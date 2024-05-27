namespace AutoOglasi.Web.Controllers
{
    using System.Diagnostics;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using AutoMapper;
    using ViewModels;
    using ViewModels.Posts;
    using Services.Posts;
    using Services.Posts.Models;

    public class HomeController : Controller
    {
        private readonly IPostsService postsService;
        private readonly IMapper mapper;
        ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> _logger, IPostsService _postsService, IMapper _mapper)
        {
            postsService = _postsService;
            mapper = _mapper;
            logger = _logger;
        }

        public IActionResult Index()
        {
            var latestPostsDTO = postsService.GetLatest(8);
            var latestPostsViewModel = mapper.Map<IEnumerable<PostInLatestListDTO>, IEnumerable<PostInLatestListViewModel>>(latestPostsDTO);

            var latestPosts = new LatestPostsViewModel()
            {
                LatestPosts = latestPostsViewModel,
            };

            return View(latestPosts);
        }




        public IActionResult About()
        {
            var latestPostsDTO = postsService.GetLatest(8);
            var latestPostsViewModel = mapper.Map<IEnumerable<PostInLatestListDTO>, IEnumerable<PostInLatestListViewModel>>(latestPostsDTO);

            var latestPosts = new LatestPostsViewModel()
            {
                LatestPosts = latestPostsViewModel,
            };

            return View(latestPosts);

        }

        public IActionResult Error(string code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, StatusCode = code });
        }

        public IActionResult StatusCode(string code)
        {
            return RedirectToAction("Error", new { code });
        }
    }
}
