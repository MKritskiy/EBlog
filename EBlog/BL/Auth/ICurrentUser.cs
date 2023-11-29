using EBlog.DAL.Models;

namespace EBlog.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedInAsync();
        Task<int?> GetCurrentUserId();
        Task<ProfileModel?> GetProfile();
        void Logout();
    }
}
