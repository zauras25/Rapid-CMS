using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Styles;

public sealed class Style : Entity<NodeId>
{
    private readonly List<StyleProperty> _properties = new();

    public IReadOnlyList<StyleProperty> Properties => _properties;

    private Style(NodeId nodeId)
        : base(nodeId)
    {
    }

    public static Style Create(NodeId nodeId)
    {
        if (nodeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Node ID cannot be empty.",
                nameof(nodeId));

        return new Style(nodeId);
    }

    public void SetProperty(StyleProperty property)
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
        _properties.RemoveAll(
            p => p.Name == name);
    }
}
