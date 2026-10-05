namespace RapidCMS.Domain.Identity;

public readonly record struct ReferenceId(Guid Value)
{
    public static ReferenceId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
