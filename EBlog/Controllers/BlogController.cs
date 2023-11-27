using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.Middleware;
using EBlog.ViewMapper;
using EBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EBlog.Controllers
{
    [SiteAuthorize()]
    public class BlogController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IBlog blog;
        public BlogController(ICurrentUser currentUser, IBlog blog)
        {
            this.currentUser = currentUser;
            this.blog = blog;
        }

        [HttpGet]
        [Route("/blogedit")]
        public IActionResult Index()
        {
            return View(new BlogViewModel());
        }

        [HttpPost]
        [Route("/blogedit")]
        public async Task<IActionResult> IndexSave(BlogViewModel model)
        {
            int? curruserid = await currentUser.GetCurrentUserId();
            if (curruserid == null)
                throw new Exception("Пользователь не найден");
            if (ModelState.IsValid)
            {
                var blogModel = BlogMapper.MapBlogViewModelToBlogModel(model);
                blogModel.UserId = (int)curruserid;
                await blog.AddOrUpdate(blogModel);
                return Redirect("/");
            }
            return View("Index", model);
        }

        [HttpGet]
        [Route("/blog/{blogid}")]
        public async Task<IActionResult> BlogPage(int blogid)
        {

            var currblog = await blog.Get(blogid);

            return View(currblog);
        }
        [HttpGet]
        [Route("/blogs")]
        public async Task<IActionResult> Blogs(int blogid)
        {

            var currblog = await blog.Search(50);

            return View(currblog);
        }
    }
}
