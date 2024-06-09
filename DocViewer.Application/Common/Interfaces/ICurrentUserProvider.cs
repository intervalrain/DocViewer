using DocViewer.Application.Common.Security.Users;

namespace DocViewer.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser CurrentUser { get; }
}

