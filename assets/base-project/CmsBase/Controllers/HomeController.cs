using System.Diagnostics;
using CMS.Utility;
using CmsBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace CmsBase.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (!SessionHelper.IsLoggedIn(HttpContext.Session))
            return RedirectToAction("LoginBasic", "Auth");

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        Response.StatusCode = statusCode ?? 500;
        ViewBag.StatusCode = statusCode;
        return View("~/Views/Shared/Error.cshtml", new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
