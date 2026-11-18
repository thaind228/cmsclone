using CMS.Utility;
using CmsBase.DataAccess;
using CmsBase.Options;
using CmsBase.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration.Json;

var builder = WebApplication.CreateBuilder(args);
// Keep environment-specific settings, environment variables, and CLI arguments above the base file.
builder.Configuration.Sources.Insert(0, new JsonConfigurationSource
{
    FileProvider = builder.Environment.ContentRootFileProvider,
    Path = "app/config/appsettings.json",
    ReloadOnChange = true
});
builder.UseCmsNLog();

const int uploadLimit = 500 * 1024 * 1024;
builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = uploadLimit);
builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = uploadLimit);

AppConfig.configuration = builder.Configuration;
builder.Services.Configure<StorageOptions>(builder.Configuration.GetSection("Storage"));
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddSingleton(new CmsDataAccess(new DBHelper(
    builder.Configuration.GetConnectionString("CMS") ?? string.Empty, 450)));

if (builder.Environment.IsDevelopment())
    builder.Services.AddDataProtection().UseEphemeralDataProtectionProvider();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

var app = builder.Build();
var pathBase = builder.Configuration["Cms:PathBase"];
if (!string.IsNullOrWhiteSpace(pathBase))
    app.UsePathBase(pathBase);

app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute("dangnhap", "dang-nhap", new { controller = "Auth", action = "LoginBasic" });
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
