using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlog blog;

        public HomeController(IBlog blog)
        {
            this.blog = blog;
        }

        public async Task<IActionResult> Index()
        {
            var bloglist = await blog.Search(4);
            var modifiedBlogList = bloglist.Select(b => {
                b.BlogContent = b.BlogContent?.Length <= 20 ? b.BlogContent : b.BlogContent?.Substring(0, 20) + "...";
                return b;
                });
            return View(modifiedBlogList);
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