using RapidCMS.Domain.Common;

namespace RapidCMS.Domain.Styles;

public sealed class StyleProperty : ValueObject
{
    public string Name { get; }

    public string Value { get; }

    private StyleProperty(
        string name,
        string value)
    {
        Name = name;
        Value = value;
    }

    public static StyleProperty Create(
        string name,
        string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Style property name cannot be empty.",
                nameof(name));

        return new StyleProperty(
            name.Trim(),
            value ?? string.Empty);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}
