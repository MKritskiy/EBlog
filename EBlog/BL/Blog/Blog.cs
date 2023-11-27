using EBlog.BL.Auth;
using EBlog.DAL;
using EBlog.DAL.Models;

namespace EBlog.BL.Blog
{
    public class Blog : IBlog
    {
        private readonly IBlogDAL blogDAL;
        private readonly ICurrentUser currentUser;

        public Blog(IBlogDAL blogDAL, ICurrentUser currentUser)
        {
            this.blogDAL = blogDAL;
            this.currentUser = currentUser;
        }

        public async Task<int?> Add(BlogModel model)
        {
            return await blogDAL.Add(model);
        }

        public async Task<BlogModel?> Get(int blogId)
        {
            return await blogDAL.Get(blogId);
        }

        public async Task<IEnumerable<BlogModel>> GetByUserId(int userId)
        {
            return await blogDAL.GetByUserId(userId);
        }

        public async Task<IEnumerable<BlogModel>> Search(int count)
        {
            return await blogDAL.Search(count);
        }

        public async Task Update(BlogModel model)
        {
            await blogDAL.Update(model);
        }

        public async Task AddOrUpdate(BlogModel model)
        {
            if (model.BlogId == null)
                await Add(model);
            else
                await Update(model);
        }

    }
}
