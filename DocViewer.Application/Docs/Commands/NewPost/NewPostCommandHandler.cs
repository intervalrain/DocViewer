using MediatR;

using ErrorOr;

using DocViewer.Domain;
using DocViewer.Application.Common.Interfaces.Persistence;

namespace DocViewer.Application.Docs.Commands.NewPost;

public class NewPostCommandHandler : IRequestHandler<NewPostCommand, ErrorOr<Doc>>
{
    private readonly IBoardRepository _boardRepository;

    public NewPostCommandHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<ErrorOr<Doc>> Handle(NewPostCommand request, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(cancellationToken);

        if (board is null)
        {
            return Error.NotFound(description: "Library not found.");
        }

        var doc = board.NewDoc(request.Title, request.Author, request.Category, request.Keywords, request.Description, request.Content, request.DateTime);

        await _boardRepository.SaveAsync(board, cancellationToken);

        return doc;
    }
}

