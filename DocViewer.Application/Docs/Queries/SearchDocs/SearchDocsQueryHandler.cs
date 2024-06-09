using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Domain;

using ErrorOr;

using MediatR;

namespace DocViewer.Application.Docs.Queries.SearchDocs;

public class SearchDocsQueryHandler : IRequestHandler<SearchDocsQuery, ErrorOr<List<Doc>>>
{
    private readonly IBoardRepository _boardRepository;

    public SearchDocsQueryHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<ErrorOr<List<Doc>>> Handle(SearchDocsQuery request, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetBoardAsync(cancellationToken);

        if (board == null)
        {
            return Error.NotFound(description: "Library not found.");
        }

        var list = board.SearchDocs(request.Text);

        return list;
    }
}

