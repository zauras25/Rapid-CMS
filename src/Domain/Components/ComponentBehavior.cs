namespace RapidCMS.Domain.Components;

public readonly record struct ComponentBehavior(string Name)
{
    public static ComponentBehavior Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Behavior name cannot be empty.",
                nameof(name));

        return new ComponentBehavior(name.Trim());
    }

    public override string ToString() => Name;
}
