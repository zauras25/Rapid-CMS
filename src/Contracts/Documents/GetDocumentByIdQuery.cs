using RapidCMS.Contracts.Queries;

namespace RapidCMS.Contracts.Documents;

public sealed record DocumentDto(
    Guid Id,
    IReadOnlyList<Guid> PageIds,
    IReadOnlyList<Guid> AssetIds,
    IReadOnlyList<Guid> VariableIds,
    IReadOnlyList<Guid> PrototypeIds,
    IReadOnlyList<Guid> ReferenceIds,
    IReadOnlyList<Guid> ComponentIds);

public sealed record GetDocumentByIdQuery(
    Guid DocumentId) : IQuery<DocumentDto>;
