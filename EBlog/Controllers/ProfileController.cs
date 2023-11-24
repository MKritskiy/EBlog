using EBlog.Middleware;
using EBlog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace EBlog.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {

        [HttpGet]
        [Route("/profile")]
        public IActionResult Index()
        {
            return View(new ProfileViewModel());
        }
        [HttpPost]
        [Route("/profile")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave()
        {
            //if (ModelState.IsValid)
            string filename = "";
            var imageData = Request.Form.Files[0];
            if (imageData != null)
            {
                MD5 md5hash = MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(imageData.FileName);
                byte[] hashBytes = md5hash.ComputeHash(inputBytes);

                string hash = Convert.ToHexString(hashBytes);

                var dir = "./wwwroot/images/" + hash.Substring(0, 2) + "/" +
                    hash.Substring(0, 4);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                filename = dir + "/" + imageData.FileName;
                using (var stream = System.IO.File.Create(filename))
                    await imageData.CopyToAsync(stream);
            }
            return View("Index", new ProfileViewModel());
        }
    }
}
