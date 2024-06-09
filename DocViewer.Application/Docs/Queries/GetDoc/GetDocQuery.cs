using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.GetDoc;

public record GetDocQuery(string UserId, int DocId) : IRequest<ErrorOr<Doc>>;