namespace DocViewer.Application.Common.Security.Users;

public class CurrentUser
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IReadOnlyList<string> Permissions { get; set; }
    public IReadOnlyList<string> Roles { get; set; }
}

