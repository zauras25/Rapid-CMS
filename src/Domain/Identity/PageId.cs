namespace RapidCMS.Domain.Identity;

public readonly struct PageId : IEquatable<PageId>
{
    public Guid Value { get; }

    public PageId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Page ID cannot be empty.", nameof(value));

        Value = value;
    }

    public static PageId New() => new(Guid.NewGuid());

    public bool Equals(PageId other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is PageId other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
    public static bool operator ==(PageId left, PageId right) => left.Equals(right);
    public static bool operator !=(PageId left, PageId right) => !left.Equals(right);
}
