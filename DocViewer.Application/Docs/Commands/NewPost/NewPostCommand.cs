using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Commands.NewPost;

public record NewPostCommand(
    string UserId,
    string Title,
    string Author,
    string Category,
    string Keywords,
    string Description,
    string Content,
    DateTime DateTime) : IRequest<ErrorOr<Doc>>;