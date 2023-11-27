using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICurrentUser currentUser;
        private readonly IBlog blog;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser, IBlog blog)
        {
            this.logger = logger;
            this.currentUser = currentUser;
            this.blog = blog;
        }

        public async Task<IActionResult> Index()
        {
            var bloglist = await blog.Search(4);
            return View(bloglist);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}