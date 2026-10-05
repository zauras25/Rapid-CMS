namespace RapidCMS.Domain.Identity;

public readonly struct StyleId : IEquatable<StyleId>
{
    public Guid Value { get; }

    public StyleId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "StyleId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static StyleId New() => new(Guid.NewGuid());

    public bool Equals(StyleId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is StyleId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        StyleId left,
        StyleId right) =>
        left.Equals(right);

    public static bool operator !=(
        StyleId left,
        StyleId right) =>
        !left.Equals(right);
}
