using DocViewer.Domain;

namespace DocViewer.Application.Common.Interfaces.Persistence;

public interface IBoardRepository
{
    Task<Board?> GetBoardAsync(CancellationToken cancellationToken);
    Task SaveAsync(Board board, CancellationToken cancellationToken);
}

