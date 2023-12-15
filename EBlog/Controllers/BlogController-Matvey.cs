using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.BL.Profile;
using EBlog.DAL.Models;
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


        [HasAuthority()]
        [HttpGet]
        [Route("/blogedit/{blogid}")]
        public async Task<IActionResult> Index(int blogid)
        {
            var currprofile = await currentUser.GetProfile();
            var currblog = await blog.Get(blogid);
            if (currprofile?.ProfileId==currblog?.ProfileId)
                return View(BlogMapper.MapBlogModelToBlogViewModel(currblog));
            throw new Exception("Вы не имеете права на это");
        }


        [HasAuthority()]
        [HttpPost]
        [Route("/blogedit/{blogid}")]
        public async Task<IActionResult> IndexDelete(int blogid)
        {
            var currprofile = await currentUser.GetProfile();
            var currblog = await blog.Get(blogid);
            if (currblog!=null && currprofile?.ProfileId == currblog?.ProfileId)
            {
                await blog.Remove(currblog ?? new BlogModel());
                return Redirect("/");
            }
            throw new Exception("Вы не имеете права на это");
        }


        [HttpPost]
        [Route("/blogedit")]
        public async Task<IActionResult> IndexSave(BlogViewModel model)
        {
            var currprofile = await currentUser.GetProfile();
            if (currprofile == null)
                throw new Exception("Профиль не найден");
            if (ModelState.IsValid)
            {
                var blogModel = BlogMapper.MapBlogViewModelToBlogModel(model);
                if (blogModel.BlogId!=null && !currprofile.Blogs.Any(b=>b.BlogId==blogModel.BlogId))
                    throw new Exception("Вы не имеете права на это");
                blogModel.ProfileId = currprofile.ProfileId ?? 0;
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
            var currProfile = await currentUser.GetProfile();
            IEnumerable<CommentViewModel>? currCommentViewModel;
            if (currcoments.Count() > 0)
            {
                currCommentViewModel = currcoments.Select(c => CommentMapper.MapCommentModelToCommentViewModel(c));
            } else
            {
                currCommentViewModel = null;
            }
            if (currblog == null)
                throw new Exception("Не удалось найти запись с таким номером");
            
            BlogPageViewModel model = new BlogPageViewModel()
                {
                    BlogViewModel = BlogMapper.MapBlogModelToBlogViewModel(currblog),
                    CommentViewModel = currCommentViewModel,
                    CurrProfileId = currProfile?.ProfileId ?? null,
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
