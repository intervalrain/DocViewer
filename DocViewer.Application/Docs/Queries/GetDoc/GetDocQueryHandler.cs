using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.GetDoc;

public class GetDocQueryHandler : IRequestHandler<GetDocQuery, ErrorOr<Doc>>
{
    private readonly IBoardRepository _boardRepository;

    public GetDocQueryHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<ErrorOr<Doc>> Handle(GetDocQuery request, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(cancellationToken);

        if (board is null)
        {
            return Error.NotFound(description: "Library not found.");
        }

        var doc = board.Docs.FirstOrDefault(doc => doc.DocId == request.DocId);

        if (doc is null)
        {
            return Error.NotFound(description: "Doc not found.");
        }

        return doc;
    }
}

