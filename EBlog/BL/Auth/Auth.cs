 using EBlog.DAL.Models;
using EBlog.DAL;
using System.ComponentModel.DataAnnotations;
using EBlog.BL.Exeption;
using EBlog.BL.General;

namespace EBlog.BL.Auth
{
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDAL;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IWebCookie webCookie;

        public Auth(IAuthDAL authDAL, 
            IEncrypt encrypt, 
            IWebCookie webCookie,
            IDbSession dbSession, IUserTokenDAL userTokenDAL) 
        {
            this.authDAL = authDAL;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
        }


        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDAL.CreateUser(user);
            await LoginAsync(id);
            return id;
        }

        public async Task LoginAsync(int id)
        {
            await dbSession.SetUserId(id);
        }



        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDAL.GetUser(email);
            if (user.UserId!=null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await LoginAsync(user.UserId ?? 0);

                if (rememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), 30);
                }

                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDAL.GetUser(email);
            if (user.UserId != null)
                throw new DuplicateEmailException();
        }
        public async Task Register(UserModel user)
        {
            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUser(user);
                scope.Complete();
            }
        }
    }
}
