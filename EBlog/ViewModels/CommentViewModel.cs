namespace EBlog.ViewModels
{
    public class CommentViewModel
    {
        public int? CommentId { get; set; }
        public string? AuthorName { get; set; }
        public string? CommentHeader { get; set; }
        public string? CommentContent { get; set; }
        public string? ProfileImage { get; set; } = "/images/default/default.jpeg";
        public int? AuthorId { get; set; } = 0;

    }
}
