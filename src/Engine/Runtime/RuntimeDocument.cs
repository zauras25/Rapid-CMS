using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeDocument
{
    public DocumentId Id { get; }

    public IReadOnlyList<RuntimePage> Pages { get; }

    private RuntimeDocument(
        DocumentId id,
        IReadOnlyList<RuntimePage> pages)
    {
        Id = id;
        Pages = pages;
    }

    public static RuntimeDocument From(
        Document document,
        IReadOnlyDictionary<PageId, RuntimePage> pages)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(pages);

        var runtimePages = new List<RuntimePage>();

        foreach (var pageId in document.PageIds)
        {
            if (!pages.TryGetValue(pageId, out var page))
            {
                throw new InvalidOperationException(
                    $"Runtime page '{pageId}' was not found.");
            }

            runtimePages.Add(page);
        }

        return new RuntimeDocument(
            document.Id,
            runtimePages);
    }

    public static RuntimeDocument From(
        Document document,
        IReadOnlyDictionary<Guid, RuntimePage> pages)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(pages);

        var converted = new Dictionary<PageId, RuntimePage>();

        foreach (var pair in pages)
            converted[new PageId(pair.Key)] = pair.Value;

        return From(document, converted);
    }
}
