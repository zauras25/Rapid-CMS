using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Nodes;

public sealed record CreateNodeCommand(
    Guid DocumentId,
    Guid PageId,
    string Name,
    Guid? ParentId) : ICommand;

public sealed record UpdateNodeCommand(
    Guid DocumentId,
    Guid PageId,
    Guid NodeId,
    string Name) : ICommand;

public sealed record MoveNodeCommand(
    Guid DocumentId,
    Guid PageId,
    Guid NodeId,
    Guid? NewParentId,
    int? Index) : ICommand;

public sealed record DeleteNodeCommand(
    Guid DocumentId,
    Guid PageId,
    Guid NodeId) : ICommand;
