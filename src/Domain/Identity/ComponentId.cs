namespace RapidCMS.Domain.Identity;

public readonly struct ComponentId : IEquatable<ComponentId>
{
    public Guid Value { get; }

    public ComponentId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "ComponentId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static ComponentId New() => new(Guid.NewGuid());

    public bool Equals(ComponentId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is ComponentId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        ComponentId left,
        ComponentId right) =>
        left.Equals(right);

    public static bool operator !=(
        ComponentId left,
        ComponentId right) =>
        !left.Equals(right);
}
