using EBlog.DAL.Models;

namespace EBlog.BL.Profile
{
    public interface IProfile
    {
        Task<ProfileModel?> Get(int userId);
        Task<int> Add(ProfileModel profile);
        Task AddOrUpdate(ProfileModel profile);
    }
}
