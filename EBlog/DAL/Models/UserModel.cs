using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EBlog.DAL.Models
{
	public class UserModel
	{
		[Key]
		public int? UserId { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Salt { get; set; } = null!;
        public string? Login { get; set; } = null;
        public string? ProfileImage { get; set; } = null;
        public int Status { get; set; } = 0;
        public string? Description { get; set; }

		public List<SessionModel>? Sessions { get; set; }
		public List<UserTokenModel>? UserTokens { get; set; }
    }
}
