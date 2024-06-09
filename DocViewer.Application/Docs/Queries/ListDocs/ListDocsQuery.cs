using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.ListDocs;

public record ListDocsQuery(string UserId, string Sort, string Filter) : IRequest<ErrorOr<Board>>;