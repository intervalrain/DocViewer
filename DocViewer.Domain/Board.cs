using DocViewer.Domain.Common;

namespace DocViewer.Domain;

public class Board : Entity
{
    private List<Doc> _docs = new List<Doc>();
    public IReadOnlyList<Doc> Docs => _docs.AsReadOnly();

    public Board()
        : base(Guid.NewGuid())
    {
    }

    public List<string> Categories => Docs.Select(doc => doc.Category).Distinct().OrderBy(x => x).ToList();

    public void AddDoc(Doc doc)
    {
        _docs.Add(doc);
    }
}

