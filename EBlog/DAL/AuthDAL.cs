using EBlog.DAL.Models;
using EBlog.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EBlog.DAL
{
    public class AuthDAL : IAuthDAL
    {
        private readonly ApplicationContext db;

        public AuthDAL()
        {
            db = new ApplicationContext();
            db.Database.EnsureCreated();
        }


        public async Task<UserModel> GetUser(string email)
        {
            return await db.Users.SingleOrDefaultAsync(u => u.Email == email) ?? new UserModel();

        }
        public async Task<UserModel> GetUser(int id)
        {
            return await db.Users.FindAsync(id) ?? new UserModel();

        }
        public async Task<int> CreateUser(UserModel model)
        {
            await db.Users.AddAsync(model);
            await db.SaveChangesAsync();
            return model.UserId ?? 0;

        }
    }
}
