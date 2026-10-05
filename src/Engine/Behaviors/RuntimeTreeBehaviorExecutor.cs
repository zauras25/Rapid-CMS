namespace RapidCMS.Engine.Behaviors;

using RapidCMS.Engine.Runtime;

public sealed class RuntimeTreeBehaviorExecutor
{
    private readonly RuntimeBehaviorExecutor _executor;

    public RuntimeTreeBehaviorExecutor(
        RuntimeBehaviorExecutor executor)
    {
        _executor = executor
            ?? throw new ArgumentNullException(nameof(executor));
    }

    public void Execute(
        RuntimeTree tree,
        RuntimeBehaviorContext context)
    {
        ArgumentNullException.ThrowIfNull(tree);
        ArgumentNullException.ThrowIfNull(context);

        Visit(tree.Root, context);
    }

    private void Visit(
        RuntimeNode node,
        RuntimeBehaviorContext context)
    {
        _executor.Execute(node, context);

        foreach (var child in node.Children)
            Visit(child, context);
    }
}
