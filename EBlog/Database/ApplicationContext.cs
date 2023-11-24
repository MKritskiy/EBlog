﻿using EBlog.DAL;
using EBlog.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EBlog.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;
        public DbSet<SessionModel> Sessions { get; set; } = null!;

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