namespace RapidCMS.Domain.Identity;

public readonly record struct StyleId(Guid Value)
{
    public static StyleId New()
    {
        return new StyleId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
