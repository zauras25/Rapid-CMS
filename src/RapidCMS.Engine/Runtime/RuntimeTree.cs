using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeTree
{
    public RuntimeNode Root { get; }

    private RuntimeTree(RuntimeNode root)
    {
        Root = root ?? throw new ArgumentNullException(nameof(root));
    }

    public static RuntimeTree From(
        Node root,
        IReadOnlyDictionary<NodeId, Node> nodes)
    {
        ArgumentNullException.ThrowIfNull(root);
        ArgumentNullException.ThrowIfNull(nodes);

        var runtimeRoot = BuildNode(root, nodes);
        return new RuntimeTree(runtimeRoot);
    }

    private static RuntimeNode BuildNode(
        Node node,
        IReadOnlyDictionary<NodeId, Node> nodes)
    {
        var runtimeNode = RuntimeNode.From(node);

        foreach (var childId in node.Children)
        {
            if (!nodes.TryGetValue(childId, out var child))
            {
                throw new InvalidOperationException(
                    $"Child node '{childId}' referenced by node '{node.Id}' was not found.");
            }

            runtimeNode.AddChild(BuildNode(child, nodes));
        }

        return runtimeNode;
    }
}
