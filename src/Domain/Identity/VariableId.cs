namespace RapidCMS.Domain.Identity;

public readonly struct VariableId : IEquatable<VariableId>
{
    public Guid Value { get; }

    public VariableId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "VariableId cannot be empty.",
                nameof(value));

        Value = value;
    }

    public static VariableId New() => new(Guid.NewGuid());

    public bool Equals(VariableId other) => Value == other.Value;

    public override bool Equals(object? obj) =>
        obj is VariableId other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        Value.ToString();

    public static bool operator ==(
        VariableId left,
        VariableId right) =>
        left.Equals(right);

    public static bool operator !=(
        VariableId left,
        VariableId right) =>
        !left.Equals(right);
}
