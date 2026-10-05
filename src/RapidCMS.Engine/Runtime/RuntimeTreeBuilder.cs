using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeTreeBuilder
{
    private readonly IReadOnlyDictionary<NodeId, Node> _nodes;

    public RuntimeTreeBuilder(IReadOnlyDictionary<NodeId, Node> nodes)
    {
        _nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
    }

    public RuntimeTree Build(NodeId rootNodeId)
    {
        if (!_nodes.TryGetValue(rootNodeId, out var root))
        {
            throw new InvalidOperationException(
                $"Root node '{rootNodeId}' was not found.");
        }

        return RuntimeTree.From(root, _nodes);
    }
}
