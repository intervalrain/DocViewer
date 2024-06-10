using DocViewer.Application.Common.Security.Request;
using DocViewer.Application.Common.Security.Users;

using ErrorOr;

namespace DocViewer.Application.Common.Interfaces;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizeableRequest<T> request,
        CurrentUser currentUser,
        string policy);
}

