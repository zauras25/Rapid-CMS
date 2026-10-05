namespace RapidCMS.Domain.Identity;

public readonly record struct TokenId(Guid Value)
{
    public static TokenId New()
    {
        return new TokenId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
