using EBlog.DAL.Models;
using EBlog.Database;
using Microsoft.EntityFrameworkCore;

namespace EBlog.DAL
{
    public class BlogDAL : IBlogDAL
    {
        private readonly ApplicationContext db;

        public BlogDAL()
        {
            db = new ApplicationContext();
            db.Database.EnsureCreated();
        }


        public async Task<int?> Add(BlogModel model)
        {
            await db.Blogs.AddAsync(model);
            await db.SaveChangesAsync();
            return model.BlogId;
        }

        public async Task<BlogModel?> Get(int blogId)
        {
            return await db.Blogs.Include(b => b.Profile).FirstOrDefaultAsync(b=>b.BlogId==blogId);
        }

        public async Task<IEnumerable<BlogModel>> GetByProfileId(int profileId)
        {
            return await db.Blogs.Include(b=>b.Profile).Where(b=>b.ProfileId == profileId).ToListAsync();
        }

        public async Task<IEnumerable<BlogModel>> Search(int count)
        {
            return await db.Blogs.Take(count).ToListAsync();
        }

        public async Task Update(BlogModel model)
        {
            db.Blogs.Update(model);
            await db.SaveChangesAsync();
        }
        public async Task Remove(BlogModel model)
        {
            db.Blogs.Remove(model);
            await db.SaveChangesAsync();
        }
    }
}
