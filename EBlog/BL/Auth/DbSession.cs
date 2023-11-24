using EBlog.BL.General;
using EBlog.DAL;
using EBlog.DAL.Models;

namespace EBlog.BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private readonly IWebCookie webCookie;
        public DbSession(IDbSessionDAL sessionDAL, IWebCookie webCookie)
        {
            this.sessionDAL = sessionDAL;
            this.webCookie = webCookie;
        }

        private void CreateSessionCookie(Guid sessionId)
        {
            webCookie.Delete(AuthConstants.SessionCookieName);
            webCookie.AddSecure(AuthConstants.SessionCookieName, sessionId.ToString());
        }

        private async Task<SessionModel> CreateSession()
        {
            var data = new SessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow
            };
            await sessionDAL.Create(data);
            return data;
        }

        private SessionModel? sessionModel = null;
        public async Task<SessionModel> GetSession()
        {
            if (sessionModel!=null)
                return sessionModel;
            Guid sessionId;
            var sessionString = webCookie.Get(AuthConstants.SessionCookieName);
            if (sessionString != null)
                sessionId = Guid.Parse(sessionString);
            else
                sessionId = Guid.NewGuid();

            var data = await this.sessionDAL.Get(sessionId);
            if (data == null)
            {
                data = await this.CreateSession();
                CreateSessionCookie(data.DbSessionId);
            }
            sessionModel = data;
            return data;
        }

        public async Task SetUserId(int userId)
        {
            var data = await this.GetSession();
            data.UserId = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);
            await sessionDAL.Create(data);
        }

        public async Task<int?> GetUserId()
        {
            var data = await this.GetSession();
            return data.UserId;
        }

        public async Task<bool> IsLoggedIn()
        {
            var data = await this.GetSession();
            return data.UserId != null;
        }


        public async Task Lock()
        {
            var data = await this.GetSession();
            await sessionDAL.Lock(data.DbSessionId);
        }

        public void ResetSessionCache()
        {
            sessionModel = null;
        }
    }
}
