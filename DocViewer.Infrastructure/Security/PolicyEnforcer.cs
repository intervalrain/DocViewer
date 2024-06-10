using DocViewer.Application.Common.Interfaces;
using DocViewer.Application.Common.Security.Request;
using DocViewer.Application.Common.Security.Users;
using ErrorOr;

namespace DocViewer.Infrastructure.Security;

public class PolicyEnforcer : IPolicyEnforcer
{
    public const string SelfOrAdmin = nameof(SelfOrAdmin);
    public const string Admin = nameof(Admin);

    public ErrorOr<Success> Authorize<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser, string policy)
    {
        return policy switch
        {
            SelfOrAdmin => SelfOrAdminPolicy(request, currentUser),
            _ => Error.Unexpected(description: "Unknown policy name")
        };
    }

    private static ErrorOr<Success> SelfOrAdminPolicy<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser)
    {
        return request.UserId == currentUser.UserId || currentUser.Roles.Contains(Admin)
            ? Result.Success
            : Error.Unauthorized(description: "Requesting user failed policy requirement");
    }
}
