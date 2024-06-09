using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.ListDocs;

public class ListDocsQueryHandler : IRequestHandler<ListDocsQuery, ErrorOr<Board>>
{
    private readonly IBoardRepository _boardRepository;

    public ListDocsQueryHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<ErrorOr<Board>> Handle(ListDocsQuery request, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(cancellationToken);

        if (board is null)
        {
            return Error.NotFound(description: "Library not found.");
        }

        return board;
    }
}

