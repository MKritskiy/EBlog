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
        [DataType(DataType.Upload)]
        [RegularExpression(@"(.jpg|.gif|.png|.jpeg|.tiff|.pbm|.bmp|.webp|.tga)$", ErrorMessage = "Некорректный формат файла")]
        public string? ProfileImage { get; set; }

    }
}
