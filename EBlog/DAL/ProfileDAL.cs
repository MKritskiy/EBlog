using EBlog.DAL.Models;
using EBlog.Database;
using Microsoft.EntityFrameworkCore;

namespace EBlog.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        private readonly ApplicationContext db;

        public ProfileDAL()
        {
            db = new ApplicationContext();
            this.db.Database.EnsureCreated();
        }


        public async Task<int?> Add(ProfileModel profile)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                await db.Profiles.AddAsync(profile);
                await db.SaveChangesAsync();
                return profile.ProfileId;
            }
        }

        public async Task<ProfileModel?> Get(int userId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.Profiles.Include(p=>p.Comments).Include(p=>p.Blogs).FirstOrDefaultAsync(p=>p.UserId==userId);
            }
        }

        public async Task Update(ProfileModel profile)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Profiles.Update(profile);
                await db.SaveChangesAsync();
            }
        }
    }
}
