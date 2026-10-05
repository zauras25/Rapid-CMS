namespace RapidCMS.Domain.Identity;

public readonly record struct ComponentId(Guid Value)
{
    public static ComponentId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
