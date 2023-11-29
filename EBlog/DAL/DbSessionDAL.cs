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
            using (ApplicationContext db = new ApplicationContext())
            {
                await db.Sessions.AddAsync(model);
                await db.SaveChangesAsync();
                return SUCCESSFUL_OPERATION;
            }
        }

        public async Task<SessionModel?> Get(Guid sessionId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.Sessions.FindAsync(sessionId);
            }
        }
        public async Task Lock(Guid sessionId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string sql = @"SELECT ""DbSessionId"" from ""Sessions"" where ""DbSessionId"" = @sessionId FOR UPDATE";
                await db.Database.ExecuteSqlRawAsync(sql, new NpgsqlParameter("sessionId", sessionId));
            }
        }
        public async Task Update(Guid dbSessionId, string sessionContent)
        { 
            using (ApplicationContext db = new ApplicationContext())
            {
                var session =  await db.Sessions.FirstOrDefaultAsync(s => s.DbSessionId == dbSessionId);
                if (session != null)
                    session.SessionContent = sessionContent;
                await db.SaveChangesAsync();
            }
        }

        public async Task Extend(Guid dbSessionId)
        {
            var session = await db.Sessions.FirstOrDefaultAsync(s => s.DbSessionId == dbSessionId);
            if (session!=null)
                session.LastAccessed = DateTime.UtcNow;
            await db.SaveChangesAsync();
        }

        public async Task Remove(Guid dbSessionId)
        {
            await db.Sessions.Where(s=>s.DbSessionId==dbSessionId).ExecuteDeleteAsync() ;
            await db.SaveChangesAsync();
        }
    }
}
