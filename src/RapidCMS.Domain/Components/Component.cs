using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.References;

namespace RapidCMS.Domain.Components;

public sealed class Component : Entity<NodeId>
{
    private readonly List<ComponentProperty> _properties = new();
    private readonly List<ComponentBehavior> _behaviors = new();
    private readonly List<NodeReference> _references = new();

    public ComponentType Type { get; private set; }

    public IReadOnlyList<ComponentProperty> Properties => _properties;

    public IReadOnlyList<ComponentBehavior> Behaviors => _behaviors;

    public IReadOnlyList<NodeReference> References => _references;

    private Component(NodeId nodeId, ComponentType type)
        : base(nodeId)
    {
        Type = type;
    }

    public static Component Create(NodeId nodeId, ComponentType type)
    {
        if (nodeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Node ID cannot be empty.",
                nameof(nodeId));

        return new Component(nodeId, type);
    }

    public void ChangeType(ComponentType type)
    {
        Type = type;
    }

    public void SetProperty(ComponentProperty property)
    {
        ArgumentNullException.ThrowIfNull(property);

        var existingIndex = _properties.FindIndex(
            p => p.Name == property.Name);

        if (existingIndex >= 0)
            _properties[existingIndex] = property;
        else
            _properties.Add(property);
    }

    public void RemoveProperty(string name)
    {
        _properties.RemoveAll(p => p.Name == name);
    }

    public void AddBehavior(ComponentBehavior behavior)
    {
        if (_behaviors.Contains(behavior))
            return;

        _behaviors.Add(behavior);
    }

    public void RemoveBehavior(ComponentBehavior behavior)
    {
        _behaviors.Remove(behavior);
    }

    public void AddReference(NodeReference reference)
    {
        ArgumentNullException.ThrowIfNull(reference);

        if (_references.Contains(reference))
            return;

        _references.Add(reference);
    }

    public void RemoveReference(NodeReference reference)
    {
        _references.Remove(reference);
    }
}
