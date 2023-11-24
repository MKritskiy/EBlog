namespace EBlog.BL.Auth
{
    public interface ICurrentUser
    {
        Task<bool> IsLoggedInAsync();
    }
}
