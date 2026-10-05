using RapidCMS.Domain.Components;
using RapidCMS.Domain.References;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeComponent
{
    private readonly Dictionary<string, string?> _properties = new();
    private readonly List<ComponentBehavior> _behaviors = new();
    private readonly List<NodeReference> _references = new();

    public string Type { get; }

    public IReadOnlyDictionary<string, string?> Properties => _properties;

    public IReadOnlyList<ComponentBehavior> Behaviors => _behaviors;

    public IReadOnlyList<NodeReference> References => _references;

    private RuntimeComponent(string type)
    {
        Type = type;
    }

    public static RuntimeComponent From(Component component)
    {
        ArgumentNullException.ThrowIfNull(component);

        var runtime = new RuntimeComponent(
            component.Type.ToString());

        foreach (var property in component.Properties)
            runtime._properties[property.Name] = property.Value;

        foreach (var behavior in component.Behaviors)
            runtime._behaviors.Add(behavior);

        foreach (var reference in component.References)
            runtime._references.Add(reference);

        return runtime;
    }
}
