namespace RapidCMS.Domain.Identity;

public readonly struct PrototypeId : IEquatable<PrototypeId>
{
    public Guid Value { get; }

    public PrototypeId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "PrototypeId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static PrototypeId New() => new(Guid.NewGuid());

    public bool Equals(PrototypeId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is PrototypeId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        PrototypeId left,
        PrototypeId right) =>
        left.Equals(right);

    public static bool operator !=(
        PrototypeId left,
        PrototypeId right) =>
        !left.Equals(right);
}
