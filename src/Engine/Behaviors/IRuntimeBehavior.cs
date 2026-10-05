using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Behaviors;

public interface IRuntimeBehavior
{
    string Name { get; }

    void Execute(
        RuntimeNode node,
        RuntimeBehaviorContext context);
}
