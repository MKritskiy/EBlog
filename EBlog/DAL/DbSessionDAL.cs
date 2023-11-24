using EBlog.DAL.Models;
using EBlog.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EBlog.DAL
{
    public class DbSessionDAL : IDbSessionDAL
    {
        private const int SUCCESSFUL_OPERATION = 0;
        private readonly ApplicationContext db;

        public DbSessionDAL()
        {
            this.db = new ApplicationContext();
            this.db.Database.EnsureCreated();

        }

        public async Task<int> Create(SessionModel model)
        {

                await db.Sessions.AddAsync(model);
                await db.SaveChangesAsync();
                return SUCCESSFUL_OPERATION;
            
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {

                return await db.Sessions.FindAsync(sessionId);
            
        }
        public async Task Lock(Guid sessionId)
        {

                string sql = @"SELECT ""DbSessionId"" from ""Sessions"" where ""DbSessionId"" = @sessionId FOR UPDATE";
                await db.Database.ExecuteSqlRawAsync(sql, new NpgsqlParameter("sessionId", sessionId));
            
        }
        public async Task<int> Update(SessionModel model)
        {

                db.Sessions.Update(model);
                await db.SaveChangesAsync();
                return SUCCESSFUL_OPERATION;
            
        }
    }
}
