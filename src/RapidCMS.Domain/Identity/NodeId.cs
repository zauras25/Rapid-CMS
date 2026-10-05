namespace RapidCMS.Domain.Identity;

public readonly record struct NodeId(Guid Value)
{
    public static NodeId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
