﻿using EBlog.BL.General;
using EBlog.BL.Profile;
using EBlog.DAL;
using EBlog.DAL.Models;

namespace EBlog.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IProfileDAL profileDAL;

        public CurrentUser(IDbSession dbSession, IWebCookie webCookie, IUserTokenDAL userTokenDAL, IProfileDAL profileDAL)
        {
            this.dbSession = dbSession;
            this.webCookie = webCookie;
            this.userTokenDAL = userTokenDAL;
            this.profileDAL = profileDAL;
        }

        public async Task<int?> GetUserIdByToken()
        {
            string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
            if (tokenCookie == null)
                return null;

            Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
            if (tokenGuid == null) 
                return null;

            int? userid = await userTokenDAL.Get((Guid)tokenGuid);

            return userid;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            bool isLoggedIn =  await dbSession.IsLoggedIn();
            if (!isLoggedIn)
            {
                int? userid = await GetUserIdByToken();
                if (userid != null)
                {
                    await dbSession.SetUserId((int)userid);
                    isLoggedIn = true;
                }
            }
            return isLoggedIn;
        } 

        public async Task<int?> GetCurrentUserId()
        {
            return await dbSession.GetUserId();
        }

        public async Task<ProfileModel?> GetProfile()
        {
            int? userid = await GetCurrentUserId();
            if (userid == null)
                throw new Exception("Пользователь не найден");

            return await profileDAL.Get((int)userid);
        }
        public void Logout()
        {
            dbSession.RemoveSessionId();
            webCookie.Delete(AuthConstants.SessionCookieName);
            webCookie.Delete(AuthConstants.RememberMeCookieName);

            dbSession.ResetSessionCache();
        }

    }
}
