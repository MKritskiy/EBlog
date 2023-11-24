using System;
using EBlog.BL.Auth;
using EBlog.DAL;
using Microsoft.AspNetCore.Http;

namespace EblogTest.Helpers
{
    public class BaseTest
    {
        protected readonly IAuthDAL authDAL = new AuthDAL();

        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        public BaseTest()
        {

            dbSession = new DbSession(dbSessionDAL, httpContextAccessor);
            authBL = new Auth(authDAL, encrypt, httpContextAccessor, dbSession);

        }
    }
}
