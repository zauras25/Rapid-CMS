using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.References;

public sealed class DocumentReference : Entity<ReferenceId>
{
    public string SourceType { get; }

    public Guid SourceId { get; }

    public string TargetType { get; }

    public Guid TargetId { get; }

    public string Relation { get; private set; }

    private DocumentReference(
        ReferenceId id,
        string sourceType,
        Guid sourceId,
        string targetType,
        Guid targetId,
        string relation)
        : base(id)
    {
        SourceType = sourceType;
        SourceId = sourceId;
        TargetType = targetType;
        TargetId = targetId;
        Relation = relation;
    }

    public static DocumentReference Create(
        string sourceType,
        Guid sourceId,
        string targetType,
        Guid targetId,
        string relation)
    {
        if (string.IsNullOrWhiteSpace(sourceType))
            throw new ArgumentException(
                "Source type cannot be empty.",
                nameof(sourceType));

        if (sourceId == Guid.Empty)
            throw new ArgumentException(
                "Source ID cannot be empty.",
                nameof(sourceId));

        if (string.IsNullOrWhiteSpace(targetType))
            throw new ArgumentException(
                "Target type cannot be empty.",
                nameof(targetType));

        if (targetId == Guid.Empty)
            throw new ArgumentException(
                "Target ID cannot be empty.",
                nameof(targetId));

        if (sourceId == targetId)
            throw new InvalidOperationException(
                "A reference cannot target itself.");

        if (string.IsNullOrWhiteSpace(relation))
            throw new ArgumentException(
                "Relation cannot be empty.",
                nameof(relation));

        return new DocumentReference(
            ReferenceId.New(),
            sourceType.Trim(),
            sourceId,
            targetType.Trim(),
            targetId,
            relation.Trim());
    }

    public void ChangeRelation(string relation)
    {
        if (string.IsNullOrWhiteSpace(relation))
            throw new ArgumentException(
                "Relation cannot be empty.",
                nameof(relation));

        Relation = relation.Trim();
    }
}
