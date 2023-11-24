using Microsoft.AspNetCore.Mvc;
using EBlog.BL.Auth;
using EBlog.ViewModels;
using EBlog.ViewMapper;
using EBlog.BL.Exeption;
using EBlog.Models;
using EBlog.Middleware;

namespace EBlog.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController : Controller
    {
        private readonly IAuth auth;
        public RegisterController(IAuth auth)
        {
            this.auth = auth;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost]
        [Route("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await auth.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateEmailException) {
                    ModelState.AddModelError("Email", "Почта уже существует");
                }

            }
            return View("Index", model);
        }
    }
}
