using DocViewer.Domain;

namespace DocViewer.Presentation.Models.Docs;

public class DocsViewModel
{
    public Board Board{ get; set; }
    public string Sort { get; set; }
    public string Filter { get; set; }

    public List<string> Categories => new[] { "All" }.Union(Board.Categories).ToList();
}

