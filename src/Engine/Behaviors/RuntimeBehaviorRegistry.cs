namespace RapidCMS.Engine.Behaviors;

public sealed class RuntimeBehaviorRegistry
{
    private readonly Dictionary<string, IRuntimeBehavior> _behaviors =
        new(StringComparer.OrdinalIgnoreCase);

    public void Register(IRuntimeBehavior behavior)
    {
        ArgumentNullException.ThrowIfNull(behavior);

        if (string.IsNullOrWhiteSpace(behavior.Name))
            throw new ArgumentException(
                "Behavior name cannot be empty.",
                nameof(behavior));

        if (!_behaviors.TryAdd(behavior.Name, behavior))
        {
            throw new InvalidOperationException(
                $"Behavior '{behavior.Name}' is already registered.");
        }
    }

    public bool TryResolve(
        string name,
        out IRuntimeBehavior? behavior)
    {
        return _behaviors.TryGetValue(name, out behavior);
    }

    public IRuntimeBehavior Resolve(string name)
    {
        if (!_behaviors.TryGetValue(name, out var behavior))
        {
            throw new InvalidOperationException(
                $"Runtime behavior '{name}' is not registered.");
        }

        return behavior;
    }
}
