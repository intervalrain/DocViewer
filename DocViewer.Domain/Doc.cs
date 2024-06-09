using DocViewer.Domain.Common;

namespace DocViewer.Domain;

public class Doc : Entity
{
    public int DocId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public IEnumerable<string> Keywords { get; set; }
    public DateTime DateTime { get; set; }
    public string Content { get; set; }
    public bool Top { get; set; }

    public DateOnly Date => DateOnly.FromDateTime(DateTime);
    public string FormatDateTime => DateTime.ToString("yyyy/MM/dd hh:mm");

    public Doc(int docId, string title, string author, string description, string category, string content, IEnumerable<string> keywords, DateTime dateTime)
        : base(Guid.NewGuid())
    {
        DocId = docId;
        Title = title;
        Author = author;
        Description = description;
        Category = category;
        Content = content;
        Keywords = keywords;
        DateTime = dateTime;
        Top = false;
    }
}

