using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBlog.DAL.Models
{
    public class UserTokenModel
    {
        [Key]
        public Guid UserTokenId { get; set; }
        public DateTime Created { get; set; }

        
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserModel? User { get; set; }
    }
}
