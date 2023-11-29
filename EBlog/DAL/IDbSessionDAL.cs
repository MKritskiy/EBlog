using EBlog.DAL.Models;

namespace EBlog.DAL
{
    public interface IDbSessionDAL
    {
        Task<SessionModel?> Get(Guid sessionId);
        Task Update(Guid dbSessionId, string sessionContent);
        Task<int> Create(SessionModel model);
        Task Lock(Guid sessionId);
        Task Extend(Guid dbSessionId);
        Task Remove(Guid dbSessionId);
    }
}
