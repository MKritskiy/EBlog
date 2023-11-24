using EBlog.DAL.Models;

namespace EBlog.DAL
{
    public interface IProfileDAL
    {
        Task<ProfileModel?> Get(int userId);
        Task<int?> Add(ProfileModel profile);
        Task Update(ProfileModel profile);


    }
}
