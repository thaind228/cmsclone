using CMS.Utility;
using CmsBase.DataAccess;
using CmsBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.ViewComponents;

public class VerticalMenuViewComponent(CmsDataAccess dataAccess) : ViewComponent
{
    private static readonly object GroupsKey = new();

    public IViewComponentResult Invoke(bool overview)
    {
        // The sidebar and dashboard can share the results of one authorization query per request.
        if (HttpContext.Items[GroupsKey] is not IReadOnlyList<CmsNavigationGroup> groups)
        {
            groups = BuildGroups();
            HttpContext.Items[GroupsKey] = groups;
        }

        return View(overview ? "Overview" : "Default", groups);
    }

    private IReadOnlyList<CmsNavigationGroup> BuildGroups()
    {
        var user = SessionHelper.GetUser(HttpContext.Session);
        if (user is null)
            return [];

        IEnumerable<(int Id, int ParentId, int Order, string Name, string? Url, bool Visible)> functions = user.IsAdmin
            ? dataAccess.SP_Functions_SelectByCondition_List(-1, "", 1, 1)
                .Select(item => (item.FunctionID, item.ParentID ?? 0, item.Order ?? 0,
                    item.FunctionName, item.Url, item.IsDisplay ?? false))
            : dataAccess.SP_Function_GetByUserID_List(int.Parse(user.Id), -1)
                .Select(item => (item.FunctionID, item.ParentID ?? 0, item.Order ?? 0,
                    item.FunctionName, item.Url, item.IsDisplay ?? false));

        var ordered = functions.OrderBy(item => item.Order).ToList();
        var children = ordered.Where(item => item.Visible && item.ParentId != 0)
            .ToLookup(item => item.ParentId);
        var currentController = HttpContext.Request.RouteValues["controller"]?.ToString();
        var currentAction = HttpContext.Request.RouteValues["action"]?.ToString();
        var groups = new List<CmsNavigationGroup>();
        foreach (var parent in ordered.Where(item => item.ParentId == 0))
        {
            var links = new List<CmsNavigationLink>();
            foreach (var child in children[parent.Id])
            {
                var route = child.Url?.Trim('/').Split('/');
                if (route is null || route.Length < 2 || string.IsNullOrWhiteSpace(route[0]) ||
                    string.IsNullOrWhiteSpace(route[1]))
                    continue;

                var isCurrent = string.Equals(currentController, route[0], StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(currentAction, route[1], StringComparison.OrdinalIgnoreCase);
                links.Add(new CmsNavigationLink(child.Name, route[0], route[1], isCurrent));
            }

            if (links.Count > 0)
                groups.Add(new CmsNavigationGroup(parent.Name, GetIcon(links[0]), links));
        }

        return groups;
    }

    private static string GetIcon(CmsNavigationLink link)
    {
        return link.Controller.ToUpperInvariant() switch
        {
            "ADMIN" => "ri-shield-user-line",
            "STORAGE" => "ri-folder-open-line",
            _ when link.Action.StartsWith("Report", StringComparison.OrdinalIgnoreCase) => "ri-bar-chart-box-line",
            _ when link.Action.StartsWith("Event", StringComparison.OrdinalIgnoreCase) => "ri-calendar-event-line",
            _ when link.Action.StartsWith("Item", StringComparison.OrdinalIgnoreCase) => "ri-gift-line",
            _ => "ri-stack-line"
        };
    }
}
