namespace RapidCMS.Domain.Identity;

public readonly struct ComponentSetId : IEquatable<ComponentSetId>
{
    public Guid Value { get; }

    public ComponentSetId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "ComponentSetId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static ComponentSetId New() => new(Guid.NewGuid());

    public bool Equals(ComponentSetId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is ComponentSetId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        ComponentSetId left,
        ComponentSetId right) =>
        left.Equals(right);

    public static bool operator !=(
        ComponentSetId left,
        ComponentSetId right) =>
        !left.Equals(right);
}
