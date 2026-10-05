namespace RapidCMS.Domain.Identity;

public readonly struct InteractionId : IEquatable<InteractionId>
{
    public Guid Value { get; }

    public InteractionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "InteractionId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static InteractionId New() => new(Guid.NewGuid());

    public bool Equals(InteractionId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is InteractionId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        InteractionId left,
        InteractionId right) =>
        left.Equals(right);

    public static bool operator !=(
        InteractionId left,
        InteractionId right) =>
        !left.Equals(right);
}
