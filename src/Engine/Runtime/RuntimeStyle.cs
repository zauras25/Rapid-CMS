using RapidCMS.Domain.Styles;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeStyle
{
    private readonly Dictionary<string, string?> _properties = new();

    public IReadOnlyDictionary<string, string?> Properties => _properties;

    private RuntimeStyle()
    {
    }

    public static RuntimeStyle From(Style style)
    {
        ArgumentNullException.ThrowIfNull(style);

        var runtime = new RuntimeStyle();

        foreach (var property in style.Properties)
            runtime._properties[property.Name] = property.Value;

        return runtime;
    }
}
