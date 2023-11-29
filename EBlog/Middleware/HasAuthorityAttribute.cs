using EBlog.BL.Auth;
using EBlog.BL.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EBlog.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class HasAuthorityAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            
            ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
            IBlog? blog = context.HttpContext.RequestServices.GetService<IBlog>();
            if (currentUser == null || blog==null)
            {
                throw new Exception("No user middleware");
            }
            var host = context.HttpContext.Request.Host.ToUriComponent();
            var path = context.HttpContext.Request.Path.ToUriComponent();
            Uri uri = new Uri(host + path);
            var querryblogid = uri.Segments.LastOrDefault().Split(new[] {';'}).First();
            int blogid;
            if (int.TryParse(querryblogid, out blogid))
            {
                var currblog = await blog.Get(blogid);
                var currprofile = await currentUser.GetProfile();
                if (currblog==null || currprofile==null || currblog?.ProfileId != currprofile?.ProfileId)
                {
                    context.Result = new RedirectResult("/");
                    return;
                }
            }
            else
            {
                context.Result = new RedirectResult("/");
                return;
            }
            await next();
        }
    }
}
