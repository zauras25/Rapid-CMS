using RapidCMS.Contracts.Queries;

namespace RapidCMS.Contracts.Nodes;

public sealed record GetNodeQuery(
    Guid DocumentId,
    Guid PageId,
    Guid NodeId) : IQuery<NodeDto>;

public sealed record GetNodeTreeQuery(
    Guid DocumentId,
    Guid PageId) : IQuery<IReadOnlyList<NodeDto>>;
