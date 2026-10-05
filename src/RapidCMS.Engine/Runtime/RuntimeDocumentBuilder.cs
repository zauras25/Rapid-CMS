using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Engine.Runtime;

public static class RuntimeDocumentBuilder
{
    public static RuntimeDocument Build(
        Document document,
        IReadOnlyDictionary<PageId, Page> pages,
        IReadOnlyDictionary<NodeId, Node> nodes)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(pages);
        ArgumentNullException.ThrowIfNull(nodes);

        var runtimePages = new Dictionary<PageId, RuntimePage>();

        foreach (var pageId in document.PageIds)
        {
            if (!pages.TryGetValue(pageId, out var page))
            {
                throw new InvalidOperationException(
                    $"Page '{pageId}' referenced by document '{document.Id}' was not found.");
            }

            runtimePages[pageId] = RuntimePage.From(
                page,
                nodes);
        }

        return RuntimeDocument.From(
            document,
            runtimePages);
    }

    public static RuntimeDocument Build(
        Document document,
        IReadOnlyDictionary<Guid, Page> pages,
        IReadOnlyDictionary<Guid, Node> nodes)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(pages);
        ArgumentNullException.ThrowIfNull(nodes);

        var pageMap = new Dictionary<PageId, Page>();

        foreach (var pair in pages)
        {
            pageMap[new PageId(pair.Key)] = pair.Value;
        }

        var nodeMap = new Dictionary<NodeId, Node>();

        foreach (var pair in nodes)
        {
            nodeMap[new NodeId(pair.Key)] = pair.Value;
        }

        return Build(
            document,
            pageMap,
            nodeMap);
    }
}
