namespace DocViewer.Application.Common.Security.Permissions;

public static partial class Permission
{
    public static class Doc
    {
        public const string Get = "get:doc";
        public const string List = "list:doc";
        public const string New = "new:doc";
    }

    public static List<string> All => new List<string>
    {
        Doc.Get,
        Doc.List,
        Doc.New
    };
}

