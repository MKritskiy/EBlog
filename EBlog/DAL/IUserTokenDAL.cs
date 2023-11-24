namespace EBlog.DAL
{
    public interface IUserTokenDAL
    {
        Task<Guid> Create(int userid);
        Task<int?> Get(Guid tokenId);
    }
}
