namespace RapidCMS.Domain.Identity;

public readonly record struct PrototypeId(Guid Value)
{
    public static PrototypeId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
