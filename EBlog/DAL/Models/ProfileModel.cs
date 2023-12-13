using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBlog.DAL.Models
{
    public class ProfileModel
    {
        [Key]
        public int? ProfileId { get; set; }
        public string? ProfileName { get; set; } = null;
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? ProfileImage { get; set; } = null;
        public string? Description { get; set; }
        public int Status { get; set; } = 0;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public UserModel? User { get; set; }

        [InverseProperty(nameof(CommentModel.Profile))]
        public List<CommentModel> Comments { get; set; } = new();

        [InverseProperty(nameof(BlogModel.Profile))]
        public List<BlogModel> Blogs { get; set; } = new();
    }
}
