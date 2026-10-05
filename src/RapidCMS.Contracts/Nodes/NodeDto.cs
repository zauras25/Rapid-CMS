namespace RapidCMS.Contracts.Nodes;

public sealed record NodeDto(
    Guid Id,
    string Name,
    Guid? ParentId,
    Guid? ComponentId,
    Guid? StyleId,
    IReadOnlyList<Guid> Children);
