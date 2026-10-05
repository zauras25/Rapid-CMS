using RapidCMS.Domain.Identity;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeReferenceResolver
{
    private readonly IReadOnlyDictionary<NodeId, RuntimeNode> _nodes;

    public RuntimeReferenceResolver(RuntimeTree tree)
    {
        ArgumentNullException.ThrowIfNull(tree);

        _nodes = Index(tree.Root);
    }

    public bool TryResolve(
        NodeId targetNodeId,
        out RuntimeNode? node)
    {
        return _nodes.TryGetValue(targetNodeId, out node);
    }

    public RuntimeNode Resolve(NodeId targetNodeId)
    {
        if (!_nodes.TryGetValue(targetNodeId, out var node))
        {
            throw new InvalidOperationException(
                $"Runtime node '{targetNodeId}' was not found.");
        }

        return node;
    }

    private static Dictionary<NodeId, RuntimeNode> Index(
        RuntimeNode root)
    {
        var result = new Dictionary<NodeId, RuntimeNode>();

        Visit(root, result);

        return result;
    }

    private static void Visit(
        RuntimeNode node,
        Dictionary<NodeId, RuntimeNode> result)
    {
        if (!result.TryAdd(node.Id, node))
        {
            throw new InvalidOperationException(
                $"Duplicate runtime node '{node.Id}' was found.");
        }

        foreach (var child in node.Children)
            Visit(child, result);
    }
}
