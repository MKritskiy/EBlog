using EBlog.BL.Auth;
using EBlog.ViewMapper;
using EBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EBlog.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuth authBL;

        public LoginController(IAuth authBL)
        {
            this.authBL = authBL;
        }
        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = await authBL.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (BL.Exeption.AuthorizationException e)
                {
                    ModelState.AddModelError("Form", "Неверные почта или пароль");
                }

            }
            return View("Index", model);
        }
    }
}
