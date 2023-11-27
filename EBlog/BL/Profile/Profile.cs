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

        public async Task<int> Add(ProfileModel profile)
        {
            var profileid = await profileDAL.Add(profile);
            return  profileid ?? 0;
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
