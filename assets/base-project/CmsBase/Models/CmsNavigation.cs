namespace CmsBase.Models;

public sealed record CmsNavigationLink(string Name, string Controller, string Action, bool IsCurrent);
public sealed record CmsNavigationGroup(string Name, string Icon, IReadOnlyList<CmsNavigationLink> Links);
