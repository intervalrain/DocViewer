﻿using DocViewer.Domain.Common;

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

    public List<string> Categories
    {
        get
        {
            var list = Docs.Select(doc => doc.Category).Distinct().OrderBy(x => x).ToList();
            list.Insert(0, "All");
            return list;
        }
    }

    public List<Doc> GetDocs(string sort, string filter)
    {
        var list = _docs;
        if (filter != "All")
        {
            list = list.Where(doc => doc.Category == filter).ToList();
        }
        if (!string.IsNullOrEmpty(sort))
        {
            list = sort switch
            {
                "DocId" => list.OrderBy(doc => doc.DocId).ToList(),
                "id_desc" => list.OrderByDescending(doc => doc.DocId).ToList(),
                "Category" => list.OrderBy(doc => doc.Category).ToList(),
                "category_desc" => list.OrderByDescending(doc => doc.Category).ToList(),
                "Title" => list.OrderBy(doc => doc.Title).ToList(),
                "title_desc" => list.OrderByDescending(doc => doc.Title).ToList(),
                "Author" => list.OrderBy(doc => doc.Author).ToList(),
                "author_desc" => list.OrderByDescending(doc => doc.Author).ToList(),
                "DateTime" => list.OrderBy(doc => doc.DateTime).ToList(),
                "datetime_desc" => list.OrderByDescending(doc => doc.DateTime).ToList(),
                _ => list
            };
        }
        return list;
    }

    private static int CalculateScore(Doc doc, string text)
    {
        int score = 0;

        if (doc.Title.Contains(text, StringComparison.CurrentCultureIgnoreCase)) score += Weighting.TitleWeight;
        if (doc.Author.Contains(text, StringComparison.CurrentCultureIgnoreCase)) score += Weighting.AuthorWeight;
        if (doc.Description.Contains(text, StringComparison.CurrentCultureIgnoreCase)) score += Weighting.DescriptionWeight;
        if (doc.Content.Contains(text, StringComparison.CurrentCultureIgnoreCase)) score += Weighting.ContentWeight;
        foreach (var keyword in doc.Keywords)
        {
            if (keyword.Contains(text, StringComparison.CurrentCultureIgnoreCase)) score += Weighting.KeywordWeight;
        }

        return score;
    }

    public List<Doc> SearchDocs(string text, int k = 10)
    {
        text = text.ToLower();

        var result = _docs
            .Select(doc => new
            {
                Doc = doc,
                Score = CalculateScore(doc, text)
            })
            .OrderByDescending(result => result.Score)
            .Take(k)
            .Select(result => result.Doc)
            .ToList();

        return result;
    }

    public Doc NewDoc(string title, string author, string category, string keywords, string description, string content, DateTime dateTime)
    {
        var doc = new Doc(
            docId: _docs.Count + 1,
            title: title,
            author: author,
            description: description,
            category: category,
            content: content,
            keywords: keywords.Split(','),
            dateTime: dateTime);
        _docs.Add(doc);
        return doc;
    }
    public async Task AddDocAsync(string raw, CancellationToken cancellationToken)
    {
        var text = await File.ReadAllTextAsync(raw, cancellationToken);
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

