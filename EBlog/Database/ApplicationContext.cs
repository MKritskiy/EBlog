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
        public DbSet<ProfileModel> Profiles { get; set; } = null!;
        public DbSet<BlogModel> Blogs { get; set; } = null!;
        public DbSet<CommentModel> Comments { get; set; } = null!;


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
