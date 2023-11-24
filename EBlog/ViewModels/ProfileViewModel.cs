using System.ComponentModel.DataAnnotations;

namespace EBlog.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? ProfileImage { get; set; }
        public int Status { get; set; } = 0;
        public string? Description { get; set; } = null;
    }
}
