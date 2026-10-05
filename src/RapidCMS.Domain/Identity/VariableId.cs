namespace RapidCMS.Domain.Identity;

public readonly record struct VariableId(Guid Value)
{
    public static VariableId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
