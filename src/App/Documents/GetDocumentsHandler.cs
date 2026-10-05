using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Documents;
using RapidCMS.Contracts.Queries;

namespace RapidCMS.Application.Documents;

public sealed class GetDocumentsHandler
    : IQueryHandler<GetDocumentsQuery, IReadOnlyList<DocumentDto>>
{
    private readonly IDocumentRepository _documentRepository;

    public GetDocumentsHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<QueryResult<IReadOnlyList<DocumentDto>>> HandleAsync(
        GetDocumentsQuery query,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var documents = await _documentRepository.GetAllAsync(
            cancellationToken);

        var result = documents
            .Select(document => new DocumentDto(
                document.Id.Value,
                document.PageIds.Select(x => x.Value).ToArray(),
                document.AssetIds.Select(x => x.Value).ToArray(),
                document.VariableIds.Select(x => x.Value).ToArray(),
                document.PrototypeIds.Select(x => x.Value).ToArray(),
                document.ReferenceIds.Select(x => x.Value).ToArray(),
                document.ComponentIds.Select(x => x.Value).ToArray()))
            .ToArray();

        return QueryResult<IReadOnlyList<DocumentDto>>.Success(result);
    }
}
