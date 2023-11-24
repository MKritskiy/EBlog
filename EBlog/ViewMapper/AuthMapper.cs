using EBlog.DAL.Models;
using EBlog.ViewModels;

namespace EBlog.ViewMapper
{
    public static class AuthMapper
    {
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model) 
        {
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}
