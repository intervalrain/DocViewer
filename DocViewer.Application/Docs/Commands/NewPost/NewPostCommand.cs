using DocViewer.Application.Common.Security.Request;
using DocViewer.Application.Common.Security.Permissions;
using DocViewer.Domain;

using ErrorOr;

namespace DocViewer.Application.Docs.Commands.NewPost;

[Authorize(Permissions = Permission.Doc.New)]
public record NewPostCommand(
    string UserId,
    string Title,
    string Author,
    string Category,
    string Keywords,
    string Description,
    string Content,
    DateTime DateTime) : IAuthorizeableRequest<ErrorOr<Doc>>;