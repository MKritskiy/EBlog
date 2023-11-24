using System.ComponentModel.DataAnnotations;

namespace EBlog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        public string? Password { get; set; }

        public bool? RememberMe { get; set; }
    }
}
