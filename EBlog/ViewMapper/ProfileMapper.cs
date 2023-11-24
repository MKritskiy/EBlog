using EBlog.DAL.Models;
using EBlog.ViewModels;

namespace EBlog.ViewMapper
{
    public static class ProfileMapper
    {
        public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        {
            return new ProfileModel()
            {
                ProfileId = model.ProfileId,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
        }
        public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        {
            return new ProfileViewModel()
            {
                ProfileId = model.ProfileId,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
        }
    }
}
