using EBlog.DAL;
using EBlog.DAL.Models;

namespace EBlog.BL.Profile
{
    public class Profile : IProfile
    {
        private readonly IProfileDAL profileDAL;

        public Profile(IProfileDAL profileDAL)
        {
            this.profileDAL = profileDAL;
        }

        public Task<int> Add(ProfileModel profile)
        {
            throw new NotImplementedException();
        }

        public async Task<ProfileModel?> Get(int userId)
        {
            return await profileDAL.Get(userId);
        }

        public async Task AddOrUpdate(ProfileModel profile)
        {
            if (profile.ProfileId == null)
                await profileDAL.Add(profile);
            else
                await profileDAL.Update(profile);
        }
    }
}
