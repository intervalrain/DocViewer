using DocViewer.Application.Common.Security.Request;
using DocViewer.Application.Common.Security.Permissions;
using DocViewer.Domain;

using ErrorOr;

namespace DocViewer.Application.Docs.Queries.GetDoc;

[Authorize(Permissions = Permission.Doc.Get)]
public record GetDocQuery(string UserId, int DocId) : IAuthorizeableRequest<ErrorOr<Doc>>;