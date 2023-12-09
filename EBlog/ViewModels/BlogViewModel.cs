using System.ComponentModel.DataAnnotations;

namespace EBlog.ViewModels
{
    public class BlogViewModel
    {
        public int? BlogId { get; set; }
        [Required]
        public string? BlogHeader { get; set; }
        [Required]
        public string? BlogContent { get; set; }
        public string? ProfileImage { get; set; } = "/images/default/default.jpeg";
        public string? AuthorName { get; set; }
        public int? AuthorId { get; set; } = 0;
    }
}
