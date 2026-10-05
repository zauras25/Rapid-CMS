using RapidCMS.Domain.References;

namespace RapidCMS.Engine.Runtime;

public sealed class RuntimeComponentReferenceResolver
{
    private readonly RuntimeReferenceResolver _resolver;

    public RuntimeComponentReferenceResolver(RuntimeTree tree)
    {
        ArgumentNullException.ThrowIfNull(tree);

        _resolver = new RuntimeReferenceResolver(tree);
    }

    public IReadOnlyList<RuntimeNode> Resolve(RuntimeComponent component)
    {
        ArgumentNullException.ThrowIfNull(component);

        var result = new List<RuntimeNode>();

        foreach (var reference in component.References)
        {
            result.Add(
                _resolver.Resolve(reference.TargetNodeId));
        }

        return result;
    }
}
