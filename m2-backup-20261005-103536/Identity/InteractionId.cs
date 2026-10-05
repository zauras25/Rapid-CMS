namespace RapidCMS.Domain.Identity;

public readonly record struct InteractionId(Guid Value)
{
    public static InteractionId New()
    {
        return new InteractionId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
