using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBlog.DAL.Models
{
	public class UserModel
	{
		[Key]
		public int? UserId { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Salt { get; set; } = null!;
        [InverseProperty(nameof(UserTokenModel.User))]
        public List<UserTokenModel> userTokens { get; set; } = new();
        [InverseProperty(nameof(SessionModel.User))]
        public List<SessionModel> sessionModels { get; set; } = new();
        [InverseProperty(nameof(ProfileModel.User))]
        public List<ProfileModel> profileModels { get; set; } = new();


    }
}
