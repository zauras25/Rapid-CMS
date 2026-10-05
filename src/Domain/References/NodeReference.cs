using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.References;

public sealed class NodeReference : ValueObject
{
    public NodeId TargetNodeId { get; }

    private NodeReference(NodeId targetNodeId)
    {
        TargetNodeId = targetNodeId;
    }

    public static NodeReference Create(NodeId targetNodeId)
    {
        if (targetNodeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Target node ID cannot be empty.",
                nameof(targetNodeId));

        return new NodeReference(targetNodeId);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return TargetNodeId;
    }
}
