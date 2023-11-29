using EBlog.DAL.Models;

namespace EBlog.BL.Auth
{
    public interface IDbSession
    {
        Task<SessionModel> GetSession();
        Task SetUserId(int userId);
        Task<int?> GetUserId();
        Task<bool> IsLoggedIn();
        Task Lock();
        void ResetSessionCache();
        Task RemoveSessionId();
    }
}
