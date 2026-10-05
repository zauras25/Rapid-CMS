namespace RapidCMS.Domain.Identity;

public readonly struct TokenId : IEquatable<TokenId>
{
    public Guid Value { get; }

    public TokenId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "TokenId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static TokenId New() => new(Guid.NewGuid());

    public bool Equals(TokenId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is TokenId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        TokenId left,
        TokenId right) =>
        left.Equals(right);

    public static bool operator !=(
        TokenId left,
        TokenId right) =>
        !left.Equals(right);
}
