using EBlog.Database;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(ServiceLifetime.Scoped);
builder.Services.AddSingleton<EBlog.BL.Auth.IEncrypt, EBlog.BL.Auth.Encrypt>();
builder.Services.AddSingleton<EBlog.DAL.IAuthDAL, EBlog.DAL.AuthDAL>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<EBlog.DAL.IDbSessionDAL, EBlog.DAL.DbSessionDAL>();
builder.Services.AddSingleton<EBlog.DAL.IUserTokenDAL, EBlog.DAL.UserTokenDAL>();
builder.Services.AddScoped<EBlog.DAL.ICommentDAL, EBlog.DAL.CommentDAL>();
builder.Services.AddScoped<EBlog.BL.Blog.IComment, EBlog.BL.Blog.Comment>();
builder.Services.AddScoped<EBlog.DAL.IBlogDAL, EBlog.DAL.BlogDAL>();
builder.Services.AddScoped<EBlog.BL.Blog.IBlog, EBlog.BL.Blog.Blog>();
builder.Services.AddScoped<EBlog.DAL.IProfileDAL, EBlog.DAL.ProfileDAL>();
builder.Services.AddScoped<EBlog.BL.Profile.IProfile, EBlog.BL.Profile.Profile>();
builder.Services.AddScoped<EBlog.BL.Auth.IAuth, EBlog.BL.Auth.Auth>();
builder.Services.AddScoped<EBlog.BL.Auth.ICurrentUser, EBlog.BL.Auth.CurrentUser>();
builder.Services.AddScoped<EBlog.BL.Auth.IDbSession, EBlog.BL.Auth.DbSession>();
builder.Services.AddScoped<EBlog.BL.General.IWebCookie, EBlog.BL.General.WebCookie>();
builder.Services.AddMvc();
var app = builder.Build();

using (ApplicationContext db = new ApplicationContext())
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
