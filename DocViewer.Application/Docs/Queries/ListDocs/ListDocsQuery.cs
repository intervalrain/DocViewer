using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.ListDocs;

public record ListDocsQuery(string UserId) : IRequest<ErrorOr<Board>>;