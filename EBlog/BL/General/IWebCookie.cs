namespace EBlog.BL.General
{
    public interface IWebCookie
    {
        void AddSecure(string cookieName, string cookieValue,int days = 0);

        void Add(string cookieName, string cookieValue, int days = 0);

        void Delete(string cookieName);

        string? Get(string cookieName);
    }
}
