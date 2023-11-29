using System.ComponentModel.DataAnnotations;

namespace EBlog.ViewModels
{
    public class ProfileViewModel
    {
        public int? ProfileId { get; set; }
        [Required]
        public string? ProfileName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }

    }
}
