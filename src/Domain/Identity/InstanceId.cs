namespace RapidCMS.Domain.Identity;

public readonly struct InstanceId : IEquatable<InstanceId>
{
    public Guid Value { get; }

    public InstanceId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "InstanceId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static InstanceId New() => new(Guid.NewGuid());

    public bool Equals(InstanceId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is InstanceId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        InstanceId left,
        InstanceId right) =>
        left.Equals(right);

    public static bool operator !=(
        InstanceId left,
        InstanceId right) =>
        !left.Equals(right);
}
