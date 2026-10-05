namespace RapidCMS.Domain.Identity;

public readonly struct DocumentId : IEquatable<DocumentId>
{
    public Guid Value { get; }

    public DocumentId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Document ID cannot be empty.", nameof(value));

        Value = value;
    }

    public static DocumentId New() => new(Guid.NewGuid());

    public bool Equals(DocumentId other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is DocumentId other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
    public static bool operator ==(DocumentId left, DocumentId right) => left.Equals(right);
    public static bool operator !=(DocumentId left, DocumentId right) => !left.Equals(right);
}
