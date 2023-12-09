using EBlog.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EBlog.BL.Auth
{
    public interface IAuth
    {
        Task<int> Authenticate(string email, string password, bool rememberMe);
        Task<int> CreateUser(EBlog.DAL.Models.UserModel user);
        Task ValidateEmail(string email);
        Task Register(UserModel user);
        Task LoginAsync(int id);
    }
}
