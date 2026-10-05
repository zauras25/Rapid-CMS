using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimePage
{
    public PageId Id { get; }

    public string Name { get; }

    public NodeId? RootNodeId { get; }

    public RuntimeNode? RootNode { get; }

    private RuntimePage(
        PageId id,
        string name,
        NodeId? rootNodeId,
        RuntimeNode? rootNode)
    {
        Id = id;
        Name = name;
        RootNodeId = rootNodeId;
        RootNode = rootNode;
    }

    public static RuntimePage From(
        Page page,
        IReadOnlyDictionary<NodeId, Node> nodes)
    {
        ArgumentNullException.ThrowIfNull(page);
        ArgumentNullException.ThrowIfNull(nodes);

        if (page.RootNodeId is not NodeId rootNodeId)
        {
            return new RuntimePage(
                page.Id,
                page.Name,
                null,
                null);
        }

        if (!nodes.TryGetValue(rootNodeId, out var rootNode))
        {
            throw new InvalidOperationException(
                $"Root node '{rootNodeId}' for page '{page.Id}' was not found.");
        }

        var runtimeTree = RuntimeTree.From(
            rootNode,
            nodes);

        return new RuntimePage(
            page.Id,
            page.Name,
            rootNodeId,
            runtimeTree.Root);
    }
}
