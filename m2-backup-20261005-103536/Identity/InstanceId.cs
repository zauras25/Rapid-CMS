namespace RapidCMS.Domain.Identity;

public readonly record struct InstanceId(Guid Value)
{
    public static InstanceId New()
    {
        return new InstanceId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
