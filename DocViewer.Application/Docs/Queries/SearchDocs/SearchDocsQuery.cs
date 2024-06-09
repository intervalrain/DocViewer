using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.SearchDocs;

public record SearchDocsQuery(string UserId, string Text) : IRequest<ErrorOr<List<Doc>>>;

