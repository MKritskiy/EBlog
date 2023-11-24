using EBlog.DAL.Models;
using EBlog.Database;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EBlog.DAL
{
    public class UserTokenDAL : IUserTokenDAL
    {
        private readonly ApplicationContext db;

        public UserTokenDAL()
        {
            db = new ApplicationContext();
            db.Database.EnsureCreated();
        }


        public async Task<Guid> Create(int userid)
        {
            Guid tokenid = Guid.NewGuid();
            string sql = @"insert into ""UserTokens"" (""UserTokenId"", ""UserId"", ""Created"") 
                    values ({0}, {1}, NOW())";

            await db.Database.ExecuteSqlRawAsync(sql, tokenid, userid);
            return tokenid;
        }

        public async Task<int?> Get(Guid tokenId)
        {
            var userToken = await db.UserTokens.FindAsync(tokenId);
            return userToken?.UserId;
        }
    }
}
