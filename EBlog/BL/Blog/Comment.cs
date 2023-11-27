using EBlog.DAL;
using EBlog.DAL.Models;
using EBlog.Database;

namespace EBlog.BL.Blog
{
    public class Comment : IComment
    {
        private readonly ICommentDAL commentDAL;
        public Comment(ICommentDAL commentDAL)
        {
            this.commentDAL = commentDAL;
        }

        public async Task<int?> Add(CommentModel model)
        {
            return await commentDAL.Add(model);
        }

        public async Task AddOrUpdate(CommentModel model)
        {
            if (model.CommentId == null)
            {
                await Add(model);
            } else
            {
                await Update(model);
            }
        }

        public async Task<CommentModel> Get(int commentId)
        {
             return await commentDAL.Get(commentId);
        }

        public async Task<IEnumerable<CommentModel>> GetByBlogId(int blogId)
        {
             return await commentDAL.GetByBlogId(blogId);
        }

        public async Task Update(CommentModel model)
        {
            await commentDAL.Update(model);
        }
    }
}
