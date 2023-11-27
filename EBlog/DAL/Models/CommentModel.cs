using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBlog.DAL.Models
{
    public class CommentModel
    {
        [Key]
        public int? CommentId { get; set; }
        public string? CommentHeader { get; set; }
        public string? CommentContent { get; set; }
        public int BlogId { get; set; }


        public int ProfileId { get; set; }
        [ForeignKey(nameof(ProfileId))]
        public ProfileModel? Profile { get; set; }
    }
}
