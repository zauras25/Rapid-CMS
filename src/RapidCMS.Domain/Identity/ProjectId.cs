namespace RapidCMS.Domain.Identity;

public readonly record struct ProjectId(Guid Value)
{
    public static ProjectId New()
    {
        return new ProjectId(Guid.NewGuid());
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
