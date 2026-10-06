using RapidCMS.Domain.Compatibility;

namespace RapidCMS.Domain.Common;

public abstract class Entity<TId>
{
    public TId Id { get; }

    public CompatibilityMetadata Compatibility { get; private set; }

    protected Entity(TId id)
    {
        Id = id;
        Compatibility = CompatibilityMetadata.Native();
    }

    public void SetCompatibility(CompatibilityMetadata compatibility)
    {
        ArgumentNullException.ThrowIfNull(compatibility);

        Compatibility = compatibility;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is not Entity<TId> other)
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id!);
    }

    public static bool operator ==(
        Entity<TId>? left,
        Entity<TId>? right)
    {
        return EqualityComparer<Entity<TId>?>.Default.Equals(
            left,
            right);
    }

    public static bool operator !=(
        Entity<TId>? left,
        Entity<TId>? right)
    {
        return !(left == right);
    }
}
