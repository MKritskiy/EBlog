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
    }
}
