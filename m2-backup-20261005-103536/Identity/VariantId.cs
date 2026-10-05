namespace RapidCMS.Domain.Identity;

public readonly record struct VariantId(Guid Value)
{
    public static VariantId New()
    {
        return new VariantId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
