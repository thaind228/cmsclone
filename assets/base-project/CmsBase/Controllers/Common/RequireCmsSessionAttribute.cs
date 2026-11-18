using CMS.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CmsBase.Controllers.Common;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequireCmsSessionAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (SessionHelper.IsLoggedIn(context.HttpContext.Session))
            return;

        context.Result = HttpMethods.IsGet(context.HttpContext.Request.Method)
            ? new RedirectToActionResult("LoginBasic", "Auth", null)
            : new UnauthorizedResult();
    }
}
