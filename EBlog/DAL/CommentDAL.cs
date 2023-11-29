using EBlog.DAL.Models;
using EBlog.Database;
using Microsoft.EntityFrameworkCore;

namespace EBlog.DAL
{
    public class CommentDAL : ICommentDAL
    {
        private readonly ApplicationContext db;

        public CommentDAL()
        {
            db = new ApplicationContext();
            db.Database.EnsureCreated();
        }

        public async Task<int?> Add(CommentModel model)
        {
            using(ApplicationContext db = new ApplicationContext()) { 
                await db.Comments.AddRangeAsync(model);
                await db.SaveChangesAsync();

                return model.CommentId;
            }
        }

        public async Task<CommentModel> Get(int commentId)
        {
            var comment = await db.Comments.Include(c=>c.Profile).FirstOrDefaultAsync(c => c.CommentId==commentId);
            return comment ?? new CommentModel();
        }

        public async Task<IEnumerable<CommentModel>> GetByBlogId(int blogid)
        {
            var comments = await db.Comments.Include(c => c.Profile).Where(c => c.BlogId == blogid).ToListAsync();
            return comments;
        }

        public async Task Update(CommentModel model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Update(model);
                await db.SaveChangesAsync();
            }
        }
        public async Task Remove(CommentModel model)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Comments.Remove(model);
                await db.SaveChangesAsync();
            }
        }
    }
}
