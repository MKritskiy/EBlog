using EBlog.DAL.Models;
using EBlog.ViewModels;

namespace EBlog.ViewMapper
{
    public class BlogMapper
    {
        public static BlogModel MapBlogViewModelToBlogModel(BlogViewModel model)
        {
            return new BlogModel()
            {
                BlogId = model.BlogId,
                BlogHeader = model.BlogHeader,
                BlogContent = model.BlogContent,
            };
        }
        public static BlogViewModel MapBlogModelToBlogViewModel(BlogModel model)
        {
            return new BlogViewModel()
            {
                BlogId = model.BlogId,
                BlogHeader = model.BlogHeader,
                BlogContent = model.BlogContent,
                AuthorName = model.Profile?.ProfileName ?? null,
                ProfileImage = model.Profile?.ProfileImage ?? "\\images\\default\\Мегумин.jpeg",
                AuthorId = model.Profile?.ProfileId,
            };
        }
    }
}
