using System;
using EBlog.BL.Auth;
using EBlog.BL.General;
using EBlog.DAL;
using Microsoft.AspNetCore.Http;

namespace EblogTest.Helpers
{
    public class BaseTest
    {
        protected readonly IAuthDAL authDAL = new AuthDAL();

        protected IEncrypt encrypt = new Encrypt();
        protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie = new TestCookie();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected ICurrentUser currentUser;

        public BaseTest()
        {

            dbSession = new DbSession(dbSessionDAL, webCookie);
            authBL = new Auth(authDAL, encrypt, webCookie, dbSession, userTokenDAL);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL);
        }
    }
}
