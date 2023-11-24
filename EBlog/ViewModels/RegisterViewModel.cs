using System.ComponentModel.DataAnnotations;

namespace EBlog.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректный формат")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*_-]).{8,}$", 
            ErrorMessage = "Пароль слишком простой")]
        public string? Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwer1234")
            {
                yield return new ValidationResult("Пароль слишком простой", new[] {"Password"});
            }
        }
    }

}
