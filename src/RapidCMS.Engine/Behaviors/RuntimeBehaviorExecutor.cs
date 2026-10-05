using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Behaviors;

public sealed class RuntimeBehaviorExecutor
{
    private readonly RuntimeBehaviorRegistry _registry;

    public RuntimeBehaviorExecutor(RuntimeBehaviorRegistry registry)
    {
        _registry = registry
            ?? throw new ArgumentNullException(nameof(registry));
    }

    public void Execute(
        RuntimeNode node,
        RuntimeBehaviorContext context)
    {
        ArgumentNullException.ThrowIfNull(node);
        ArgumentNullException.ThrowIfNull(context);

        if (node.Component is null)
            return;

        foreach (var behavior in node.Component.Behaviors)
        {
            var runtimeBehavior = _registry.Resolve(behavior.Name);

            runtimeBehavior.Execute(
                node,
                context);
        }
    }
}
