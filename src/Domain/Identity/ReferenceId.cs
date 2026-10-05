namespace RapidCMS.Domain.Identity;

public readonly struct ReferenceId : IEquatable<ReferenceId>
{
    public Guid Value { get; }

    public ReferenceId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "ReferenceId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static ReferenceId New() => new(Guid.NewGuid());

    public bool Equals(ReferenceId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is ReferenceId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        ReferenceId left,
        ReferenceId right) =>
        left.Equals(right);

    public static bool operator !=(
        ReferenceId left,
        ReferenceId right) =>
        !left.Equals(right);
}
