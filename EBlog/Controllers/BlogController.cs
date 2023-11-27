using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.BL.Profile;
using EBlog.Middleware;
using EBlog.ViewMapper;
using EBlog.ViewModels;
using EComment.ViewMapper;
using Microsoft.AspNetCore.Mvc;

namespace EBlog.Controllers
{
    [SiteAuthorize()]
    public class BlogController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IBlog blog;
        private readonly IComment comment;
        private readonly IProfile profile;
        public BlogController(ICurrentUser currentUser, IBlog blog, IComment comment, IProfile profile)
        {
            this.currentUser = currentUser;
            this.blog = blog;
            this.comment = comment;
            this.profile = profile;
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
            var currcoments = await comment.GetByBlogId(blogid);
            IEnumerable<CommentViewModel>? currCommentViewModel;
            if (currcoments.Count() > 0)
            {
                currCommentViewModel = currcoments.Select(c => CommentMapper.MapCommentModelToCommentViewModel(c));
            } else
            {
                currCommentViewModel = null;
            }
            BlogPageViewModel model = new BlogPageViewModel()
                {
                    BlogViewModel = BlogMapper.MapBlogModelToBlogViewModel(currblog),
                    CommentViewModel = currCommentViewModel,
                };
            return View(model);
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
