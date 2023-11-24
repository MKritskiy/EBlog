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




    }
}
