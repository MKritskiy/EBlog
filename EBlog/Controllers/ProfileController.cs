using EBlog.BL.Auth;
using EBlog.BL.General;
using EBlog.BL.Profile;
using EBlog.DAL.Models;
using EBlog.Middleware;
using EBlog.Service;
using EBlog.ViewMapper;
using EBlog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EBlog.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;

        public ProfileController(ICurrentUser currentUser, IProfile profile)
        {
            this.currentUser = currentUser;
            this.profile = profile;
        }
        [HttpGet]
        [Route("/profile")]
        public async Task<IActionResult> Index()
        {
            var profileModel = await currentUser.GetProfile();

            var profileViewModel = profileModel!=null ? ProfileMapper.MapProfileModelToProfileViewModel(profileModel) : new ProfileViewModel();

            return View(profileViewModel);
        }
        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(ProfileViewModel model)
        {
            int? curruserid = await currentUser.GetCurrentUserId();
            if (curruserid == null)
                throw new Exception("Пользователь не найден");
           
            var currprofile = await profile.Get((int)curruserid);

            if (currprofile?.ProfileId != model.ProfileId)
                throw new Exception("Error");
            ProfileModel profileModel = ProfileMapper.MapProfileViewModelToProfileModel(model);
            profileModel.UserId = (int)curruserid;
            profileModel.ProfileImage = currprofile?.ProfileImage;
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count > 0 && Request.Form.Files[0] != null)
                {
                    var isValidExtension = Regex.IsMatch(Request.Form.Files[0].FileName, @"(.jpg|.gif|.png|.jpeg|.tiff|.pbm|.bmp|.webp|.tga)$");
                    if (!isValidExtension)
                    {
                        ModelState.AddModelError("ProfileImage", "Некорректный формат файла");
                        return View("Index", ProfileMapper.MapProfileModelToProfileViewModel(profileModel));
                    }
                    WebFile webFile = new WebFile();
                    string filename = webFile.GetWebFileName(Request.Form.Files[0].FileName, Request.Form.Files[0].OpenReadStream());
                    await webFile.UploadAndResizeImage(Request.Form.Files[0].OpenReadStream(), filename, 800, 600);
                    profileModel.ProfileImage = filename;
                }
                await profile.AddOrUpdate(profileModel);
            }
            return View("Index", ProfileMapper.MapProfileModelToProfileViewModel(profileModel));
        }


        [Route("/profile/logout")]
        public ActionResult Logout()
        {
            currentUser.Logout();
            return Redirect("/");
        }
    }
}
