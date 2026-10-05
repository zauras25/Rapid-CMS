namespace RapidCMS.Domain.Components;

public readonly record struct ComponentType(string Value)
{
    public static ComponentType Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "Component type cannot be empty.",
                nameof(value));

        return new ComponentType(value.Trim());
    }

    public override string ToString() => Value;
}
