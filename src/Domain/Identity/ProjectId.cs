namespace RapidCMS.Domain.Identity;

public readonly struct ProjectId : IEquatable<ProjectId>
{
    public Guid Value { get; }

    public ProjectId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "ProjectId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static ProjectId New() => new(Guid.NewGuid());

    public bool Equals(ProjectId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is ProjectId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        ProjectId left,
        ProjectId right) =>
        left.Equals(right);

    public static bool operator !=(
        ProjectId left,
        ProjectId right) =>
        !left.Equals(right);
}
