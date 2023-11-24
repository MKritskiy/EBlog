using System.ComponentModel.DataAnnotations;

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
    }
}
