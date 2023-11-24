using System.ComponentModel.DataAnnotations;

namespace EBlog.DAL.Models
{
    public class UserTokenModel
    {
        [Key]
        public Guid UserTokenId { get; set; }
        public DateTime Created { get; set; }

        public int? UserId { get; set; }
        public UserModel? User { get; set; }
    }
}
