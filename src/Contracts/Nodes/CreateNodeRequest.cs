namespace RapidCMS.Contracts.Nodes;

public sealed record CreateNodeRequest(
    string Name,
    Guid? ParentId);
