using EBlog.DAL.Models;

namespace EBlog.BL.Blog
{
    public interface IComment
    {
        public Task<CommentModel> Get(int commentId);
        public Task<IEnumerable<CommentModel>> GetByBlogId(int blogId);
        public Task<int?> Add(CommentModel model);
        public Task Update(CommentModel model);
        public Task AddOrUpdate(CommentModel model);
    }
}
