using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBlog.DAL.Models
{
    public class SessionModel
    {
        [Key]
        public Guid DbSessionId { get; set; }

        public string? SessionContent { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastAccessed { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserModel? User { get; set; }
    }
}
