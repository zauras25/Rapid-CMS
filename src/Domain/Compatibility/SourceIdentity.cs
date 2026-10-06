namespace RapidCMS.Domain.Compatibility;

public sealed class SourceIdentity : IEquatable<SourceIdentity>
{
    public string Source { get; }
    public string SourceId { get; }

    private SourceIdentity(string source, string sourceId)
    {
        Source = source;
        SourceId = sourceId;
    }

    public static SourceIdentity Create(string source, string sourceId)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException(
                "Source cannot be empty.",
                nameof(source));

        if (string.IsNullOrWhiteSpace(sourceId))
            throw new ArgumentException(
                "Source ID cannot be empty.",
                nameof(sourceId));

        return new SourceIdentity(
            source.Trim(),
            sourceId.Trim());
    }

    public bool Equals(SourceIdentity? other)
    {
        if (other is null)
            return false;

        return string.Equals(
                   Source,
                   other.Source,
                   StringComparison.OrdinalIgnoreCase)
               &&
               string.Equals(
                   SourceId,
                   other.SourceId,
                   StringComparison.Ordinal);
    }

    public override bool Equals(object? obj) =>
        obj is SourceIdentity other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(
            Source.ToUpperInvariant(),
            SourceId);

    public override string ToString() =>
        $"{Source}:{SourceId}";
}
