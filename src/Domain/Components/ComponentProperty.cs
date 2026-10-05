using RapidCMS.Domain.Common;

namespace RapidCMS.Domain.Components;

public sealed class ComponentProperty : ValueObject
{
    public string Name { get; }
    public string Value { get; }

    private ComponentProperty(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public static ComponentProperty Create(string name, string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Property name cannot be empty.",
                nameof(name));

        return new ComponentProperty(name.Trim(), value ?? string.Empty);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}
