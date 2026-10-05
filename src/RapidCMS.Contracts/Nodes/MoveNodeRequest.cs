namespace RapidCMS.Contracts.Nodes;

public sealed record MoveNodeRequest(
    Guid? NewParentId,
    int? Index);
