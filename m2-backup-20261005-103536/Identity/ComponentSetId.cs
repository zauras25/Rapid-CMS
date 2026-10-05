namespace RapidCMS.Domain.Identity;

public readonly record struct ComponentSetId(Guid Value)
{
    public static ComponentSetId New()
    {
        return new ComponentSetId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
