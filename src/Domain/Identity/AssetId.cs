namespace RapidCMS.Domain.Identity;

public readonly struct AssetId : IEquatable<AssetId>
{
    public Guid Value { get; }

    public AssetId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "AssetId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static AssetId New() => new(Guid.NewGuid());

    public bool Equals(AssetId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is AssetId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        AssetId left,
        AssetId right) =>
        left.Equals(right);

    public static bool operator !=(
        AssetId left,
        AssetId right) =>
        !left.Equals(right);
}
