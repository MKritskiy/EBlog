using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.BL.Profile;
using EBlog.DAL.Models;
using EBlog.ViewModels;
using EComment.ViewMapper;
using Microsoft.AspNetCore.Mvc;

namespace EBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IBlog blog;
        private readonly IComment comment;
        public CommentController(ICurrentUser currentUser, IBlog blog, IComment comment)
        {
            this.currentUser = currentUser;
            this.blog = blog;
            this.comment = comment;
        }
        [HttpPost]
        [Route("/blog/{blogid}/commentpost")]
        public async Task<IActionResult> CommentPost(int blogid, CommentViewModel model)
        {
            int? curruserid = await currentUser.GetCurrentUserId();
            if (curruserid == null)
                throw new Exception("Пользователь не найден");

            var authorprofile = await currentUser.GetProfile();

            if (authorprofile==null)
                throw new Exception("Error");
            if (ModelState.IsValid)
            {
                CommentModel currcomment = CommentMapper.MapCommentViewModelToCommentModel(model);
                currcomment.ProfileId = authorprofile.ProfileId ?? 0;
                currcomment.BlogId = blogid;
                await comment.AddOrUpdate(currcomment);

            }
            return Redirect("/blog/"+blogid);
        }
        [HttpPost]
        [Route("/blog/{blogId}/editcomment/{commentId}")]
        public async Task<IActionResult> EditComment(int blogId, int commentId, string editedContent)
        {
            var currcomment = await comment.Get(commentId);
            if (currcomment == null || currcomment.ProfileId != (await currentUser.GetProfile())?.ProfileId)
            {
                return BadRequest(); // Вернуть ошибку или другой статус, если комментарий не найден или пользователь не имеет права редактировать его
            }

            currcomment.CommentContent = editedContent;
            await comment.Update(currcomment);

            return Redirect("/blog/"+blogId);
        }
        [HttpPost]
        [Route("/blog/{blogId}/deletecomment/{commentId}")]
        public async Task<IActionResult> RemoveComment(int blogId, int commentId)
        {
            var currcomment = await comment.Get(commentId);
            if (currcomment == null || currcomment.ProfileId != (await currentUser.GetProfile())?.ProfileId)
            {
                return BadRequest(); // Вернуть ошибку или другой статус, если комментарий не найден или пользователь не имеет права редактировать его
            }
 
            await comment.Remove(currcomment);

            return Redirect("/blog/" + blogId);
        }
    }
}
