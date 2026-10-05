namespace RapidCMS.Domain.Identity;

public readonly struct VariantId : IEquatable<VariantId>
{
    public Guid Value { get; }

    public VariantId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "VariantId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static VariantId New() => new(Guid.NewGuid());

    public bool Equals(VariantId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is VariantId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        VariantId left,
        VariantId right) =>
        left.Equals(right);

    public static bool operator !=(
        VariantId left,
        VariantId right) =>
        !left.Equals(right);
}
