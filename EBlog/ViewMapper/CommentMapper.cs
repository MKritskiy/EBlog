using EBlog.DAL.Models;
using EBlog.ViewModels;

namespace EComment.ViewMapper
{
    public class CommentMapper
    {
        public static CommentModel MapCommentViewModelToCommentModel(CommentViewModel model)
        {
            return new CommentModel()
            {
                CommentId = model.CommentId,
                CommentHeader = model.CommentHeader,
                CommentContent = model.CommentContent,
            };
        }
        public static CommentViewModel MapCommentModelToCommentViewModel(CommentModel model)
        {
            return new CommentViewModel()
            {
                CommentId = model.CommentId,
                AuthorName = model.Profile?.ProfileName ?? null,
                ProfileImage = model.Profile?.ProfileImage ?? "\\images\\default\\Мегумин.jpeg",
                CommentHeader = model.CommentHeader,
                CommentContent = model.CommentContent,
                AuthorId = model.Profile?.ProfileId,
            };
        }
    }
}
