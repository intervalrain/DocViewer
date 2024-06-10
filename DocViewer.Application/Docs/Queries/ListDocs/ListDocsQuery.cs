using DocViewer.Application.Common.Security.Request;
using DocViewer.Application.Common.Security.Permissions;

using DocViewer.Domain;

using ErrorOr;
using MediatR;

namespace DocViewer.Application.Docs.Queries.ListDocs;

[Authorize(Permissions = Permission.Doc.Get)]
public record ListDocsQuery(string UserId) : IAuthorizeableRequest<ErrorOr<Board>>;