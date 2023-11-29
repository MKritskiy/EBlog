namespace EBlog.ViewModels
{
    public class BlogPageViewModel
    {
        public BlogViewModel? BlogViewModel { get; set; } = null!; 
        public IEnumerable<CommentViewModel>? CommentViewModel { get; set; }
        public int? CurrProfileId;
    }
}
