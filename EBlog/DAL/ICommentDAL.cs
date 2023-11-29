using EBlog.DAL.Models;

namespace EBlog.DAL
{
    public interface ICommentDAL
    {
        public Task<CommentModel> Get(int commentId);
        public Task<IEnumerable<CommentModel>> GetByBlogId(int blogid);
        public Task<int?> Add(CommentModel model);
        public Task Update(CommentModel model);
        public Task Remove(CommentModel model);

    }
}
