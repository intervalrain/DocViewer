using DocViewer.Application.Common.Interfaces.Persistence;
using DocViewer.Domain;

namespace DocViewer.Infrastructure.Common.Persistence;

public class BoardRepository : IBoardRepository
{
    private const string path = "/Users/rainhu/workspace/DocViewer/Markdowns";
    private static Board _board; 

    public BoardRepository()
    {
        _board = new Board();
        LoadMarkdownFiles();
    }

    public Board Get()
    {
        return _board;
    }

    private void LoadMarkdownFiles()
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"The directory {path} not found.");
        }

        var directoryInfo = new DirectoryInfo(path);
        var files = directoryInfo.GetFiles("*.md").OrderBy(f => f.CreationTime).ToArray();

        foreach (var file in files)
        {
            _board.AddDoc(file.FullName);
        }
    }
}