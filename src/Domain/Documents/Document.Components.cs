using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Documents;

public sealed partial class Document
{
    public void AddComponent(ComponentId componentId)
    {
        if (componentId.Value == Guid.Empty)
            throw new ArgumentException(
                "Component ID cannot be empty.",
                nameof(componentId));

        if (ComponentIds.Contains(componentId))
            throw new InvalidOperationException(
                $"Component '{componentId.Value}' is already attached to this document.");

        _componentIds.Add(componentId);
    }

    public bool RemoveComponent(ComponentId componentId)
    {
        if (componentId.Value == Guid.Empty)
            throw new ArgumentException(
                "Component ID cannot be empty.",
                nameof(componentId));

        return _componentIds.Remove(componentId);
    }
}

