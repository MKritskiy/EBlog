using EBlog.DAL;
using EBlog.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EBlog.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<SessionModel> Sessions { get; set; } = null!;
        public DbSet<UserTokenModel> UserTokens { get; set; } = null!;
        public DbSet<ProfileModel> Profiles { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DbHelper.ConnString);
        }
    }
}
