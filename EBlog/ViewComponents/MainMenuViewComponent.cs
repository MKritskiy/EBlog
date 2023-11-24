using EBlog.BL.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EBlog.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly ICurrentUser currentUser;
        public MainMenuViewComponent(ICurrentUser currentUser)
        {
            this.currentUser=currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            bool isLoggedIn = await currentUser.IsLoggedInAsync();
            return View("Index", isLoggedIn);
        }

    }
}
