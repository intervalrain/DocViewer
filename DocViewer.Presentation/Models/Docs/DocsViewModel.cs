using DocViewer.Domain;

namespace DocViewer.Presentation.Models.Docs;

public class DocsViewModel
{
    public List<Doc> Docs { get; set; }
    public string Sort { get; set; }
    public string Filter { get; set; }
    public List<string> Categories { get; set; }
}

