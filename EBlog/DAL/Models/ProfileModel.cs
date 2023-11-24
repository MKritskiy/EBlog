using System.ComponentModel.DataAnnotations;

namespace EBlog.DAL.Models
{
    public class ProfileModel
    {
        [Key]
        public int? ProfileId { get; set; }
        public string? Login { get; set; } = null;
        public string? ProfileImage { get; set; } = null;
        public int Status { get; set; } = 0;
        public string? Description { get; set; }
    }
}
