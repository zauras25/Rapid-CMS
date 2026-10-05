namespace RapidCMS.Domain.Identity;

public readonly struct NodeId : IEquatable<NodeId>
{
    public Guid Value { get; }

    public NodeId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Node ID cannot be empty.", nameof(value));

        Value = value;
    }

    public static NodeId New() => new(Guid.NewGuid());

    public bool Equals(NodeId other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is NodeId other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
    public static bool operator ==(NodeId left, NodeId right) => left.Equals(right);
    public static bool operator !=(NodeId left, NodeId right) => !left.Equals(right);
}
