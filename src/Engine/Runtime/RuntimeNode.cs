using RapidCMS.Domain.Components;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeNode
{
    private readonly List<RuntimeNode> _children = new();

    public NodeId Id { get; }
    public string Name { get; }
    public RuntimeComponent? Component { get; }
    public RuntimeStyle? Style { get; }

    public IReadOnlyList<RuntimeNode> Children => _children;

    private RuntimeNode(
        NodeId id,
        string name,
        RuntimeComponent? component,
        RuntimeStyle? style)
    {
        Id = id;
        Name = name;
        Component = component;
        Style = style;
    }

    public static RuntimeNode From(Node node)
    {
        ArgumentNullException.ThrowIfNull(node);

        var runtime = new RuntimeNode(
            node.Id,
            node.Name,
            node.Component is null
                ? null
                : RuntimeComponent.From(node.Component),
            node.Style is null
                ? null
                : RuntimeStyle.From(node.Style));

        return runtime;
    }

    internal void AddChild(RuntimeNode child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (_children.Any(x => x.Id == child.Id))
            return;

        _children.Add(child);
    }
}
