using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBlog.DAL.Models
{
    public class BlogModel
    {
        [Key]
        public int? BlogId { get; set; }
        public string? BlogHeader { get; set; }
        public string? BlogContent { get; set; }


        public int ProfileId { get; set; }
        [ForeignKey(nameof(ProfileId))]
        public ProfileModel? Profile { get; set; }
    }
}
