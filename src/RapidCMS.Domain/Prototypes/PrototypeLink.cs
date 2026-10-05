using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Prototypes;

public sealed class PrototypeLink : Entity<PrototypeId>
{
    public NodeId SourceNodeId { get; }

    public NodeId TargetNodeId { get; }

    public string Action { get; private set; }

    private PrototypeLink(
        PrototypeId id,
        NodeId sourceNodeId,
        NodeId targetNodeId,
        string action)
        : base(id)
    {
        SourceNodeId = sourceNodeId;
        TargetNodeId = targetNodeId;
        Action = action;
    }

    public static PrototypeLink Create(
        NodeId sourceNodeId,
        NodeId targetNodeId,
        string action)
    {
        if (sourceNodeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Source node ID cannot be empty.",
                nameof(sourceNodeId));

        if (targetNodeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Target node ID cannot be empty.",
                nameof(targetNodeId));

        if (sourceNodeId == targetNodeId)
            throw new InvalidOperationException(
                "A prototype link cannot target its source node.");

        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException(
                "Prototype action cannot be empty.",
                nameof(action));

        return new PrototypeLink(
            PrototypeId.New(),
            sourceNodeId,
            targetNodeId,
            action.Trim());
    }

    public void ChangeAction(string action)
    {
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException(
                "Prototype action cannot be empty.",
                nameof(action));

        Action = action.Trim();
    }
}
