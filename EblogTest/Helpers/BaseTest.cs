using System;
using EBlog.BL.Auth;
using EBlog.BL.Blog;
using EBlog.BL.General;
using EBlog.BL.Profile;
using EBlog.DAL;

namespace EblogTest.Helpers
{
    public class BaseTest
    {
        protected readonly IAuthDAL authDAL = new AuthDAL();

        protected IEncrypt encrypt = new Encrypt();
        protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected ICurrentUser currentUser;
        protected IProfileDAL profileDAL = new ProfileDAL();
        protected IProfile profile;
        protected IBlogDAL blogDAL = new BlogDAL();
        protected IBlog blog;
        protected ICommentDAL commentDAL = new CommentDAL();
        protected IComment comment;

        public BaseTest()
        {
            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            profile = new Profile(profileDAL);
            authBL = new Auth(authDAL, encrypt, webCookie, dbSession, userTokenDAL, profile);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, profileDAL);
            blog = new Blog(blogDAL, currentUser);
            comment = new Comment(commentDAL);

        }
    }
}
