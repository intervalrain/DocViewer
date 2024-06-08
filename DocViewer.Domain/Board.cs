using System;

using DocViewer.Domain.Common;

using YamlDotNet.Core.Tokens;
using YamlDotNet.Serialization;

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

    public void AddDoc(string raw)
    {
        var text = File.ReadAllText(raw);
        var yaml = text.Split("---")[1].Trim();
        var content = text.Substring(text.IndexOf("---", yaml.Length) + 3).Trim();

        var deserializer = new DeserializerBuilder().Build();
        var metadata = deserializer.Deserialize<Dictionary<string, object>>(yaml);

        int docId = _docs.Count + 1;
        var title = metadata.TryGetValue("title", out object? value1) ? value1.ToString() : "Untitiled";
        var keywords = metadata.TryGetValue("keywords", out object? value2) ? new List<string>(((List<object>)value2).ConvertAll(k => k.ToString())) : new List<string>();
        var description = metadata.TryGetValue("description", out object? value3) ? value3.ToString() : string.Empty;
        var dateTime = metadata.TryGetValue("date", out object? value4) ? DateTime.Parse(value4.ToString()) : DateTime.Now;
        var author = metadata.TryGetValue("author", out object? value5) ? value5.ToString() : "Unknown";
        var category = metadata.TryGetValue("Categories", out object? value6) ? value6.ToString() : "General";

        var doc = new Doc(
            docId: docId,
            title: title,
            author: author,
            description: description,
            category: category,
            content: content,
            keywords: keywords,
            dateTime: dateTime);

        _docs.Add(doc);
    }
}

