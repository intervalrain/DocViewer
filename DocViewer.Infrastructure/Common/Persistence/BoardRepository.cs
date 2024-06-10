using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Domain;

namespace DocViewer.Infrastructure.Common.Persistence;

public class BoardRepository : IBoardRepository
{
    private const string path = "/Users/rainhu/workspace/DocViewer/Markdowns";
    private static Board _board;

    public async Task<Board?> GetBoardAsync(CancellationToken cancellationToken)
    {
        if (_board == null)
        {
            return _board = await InitialBoard(cancellationToken);
        }
        return _board;
    }

    public async Task SaveAsync(Board board, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"The directory {path} not found.");
        }

        var doc = board.Docs.LastOrDefault();

        if (doc == null)
        {
            throw new Exception();
        }

        var raw = doc.ToString();

        var filePath = Path.Combine(path, $"{doc.Title.Replace(" ", "")}.md");
        await File.WriteAllTextAsync(filePath, raw, cancellationToken);
    }

    private async Task<Board> InitialBoard(CancellationToken cancellationToken)
    {
        var board = new Board();
        await LoadMarkdownFiles(board, cancellationToken);
        return board;
    }

    private async Task LoadMarkdownFiles(Board board, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"The directory {path} not found.");
        }

        var directoryInfo = new DirectoryInfo(path);
        var files = directoryInfo.GetFiles("*.md").OrderBy(f => f.CreationTime).ToArray();

        foreach (var file in files)
        {
            await board.AddDocAsync(file.FullName, cancellationToken);
        }
    }
}