using RapidCMS.Contracts.Queries;

namespace RapidCMS.Contracts.Documents;

public sealed record GetDocumentsQuery
    : IQuery<IReadOnlyList<DocumentDto>>;
