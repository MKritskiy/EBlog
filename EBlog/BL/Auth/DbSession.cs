using EBlog.BL.General;
using EBlog.DAL;
using EBlog.DAL.Models;
using System.Text.Json;

namespace EBlog.BL.Auth
{
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private readonly IWebCookie webCookie;

        private Dictionary<string, object> SessionContent = new Dictionary<string, object>();
        private SessionModel? sessionModel = null;
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

        public async Task UpdateSessionData()
        {
            if (this.sessionModel != null)
                await this.sessionDAL.Update(this.sessionModel.DbSessionId, JsonSerializer.Serialize(SessionContent));
            else
                throw new Exception("Сессия не загружена");
        }
        public void AddValue(string key, object value)
        {
            if (SessionContent.ContainsKey(key))
                SessionContent[key] = value;
            else
                SessionContent.Add(key, value);
        }
        public void RemoveValue(string key)
        {
            if (SessionContent.ContainsKey(key))
                SessionContent.Remove(key);
        }
        public object GetValueDef(string key, object defaultValue)
        {
            if (SessionContent.ContainsKey(key))
                return SessionContent[key];
            return defaultValue;
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
            if (data.SessionContent != null)
                SessionContent = JsonSerializer.Deserialize<Dictionary<string, object>>(data.SessionContent) ?? new Dictionary<string, object>();
            await this.sessionDAL.Extend(data.DbSessionId);
            return data;
        }

        public async Task SetUserId(int userId)
        {
            var data = await this.GetSession();
            data.UserId = userId;
            data.DbSessionId = Guid.NewGuid();
            CreateSessionCookie(data.DbSessionId);
            data.SessionContent = JsonSerializer.Serialize(SessionContent);
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
