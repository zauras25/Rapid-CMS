using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Contracts.Queries;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class GetDocumentByIdHandler
    : IQueryHandler<GetDocumentByIdQuery, DocumentDto>
{
    private readonly IDocumentRepository _documentRepository;

    public GetDocumentByIdHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<QueryResult<DocumentDto>> HandleAsync(
        GetDocumentByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (query.DocumentId == Guid.Empty)
        {
            return QueryResult<DocumentDto>.Failure(
                "Document ID cannot be empty.");
        }

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(query.DocumentId),
            cancellationToken);

        if (document is null)
        {
            return QueryResult<DocumentDto>.Failure(
                $"Document '{query.DocumentId}' was not found.");
        }

        var result = new DocumentDto(
            document.Id.Value,
            document.PageIds.Select(x => x.Value).ToArray(),
            document.AssetIds.Select(x => x.Value).ToArray(),
            document.VariableIds.Select(x => x.Value).ToArray(),
            document.PrototypeIds.Select(x => x.Value).ToArray(),
            document.ReferenceIds.Select(x => x.Value).ToArray(),
            document.ComponentIds.Select(x => x.Value).ToArray());

        return QueryResult<DocumentDto>.Success(result);
    }
}
