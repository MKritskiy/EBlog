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
            };
        }
    }
}
