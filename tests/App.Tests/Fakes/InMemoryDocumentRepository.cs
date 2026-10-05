using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests.Fakes;

public sealed class InMemoryDocumentRepository : IDocumentRepository
{
    private readonly Dictionary<Guid, Document> _documents = new();

    public Task AddAsync(
        Document document,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _documents.Add(
            document.Id.Value,
            document);

        return Task.CompletedTask;
    }

    public Task<Document?> GetByIdAsync(
        DocumentId documentId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _documents.TryGetValue(
            documentId.Value,
            out var document);

        return Task.FromResult(document);
    }

    public Task<IReadOnlyList<Document>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IReadOnlyList<Document> result =
            _documents.Values.ToArray();

        return Task.FromResult(result);
    }

    public Task UpdateAsync(
        Document document,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_documents.ContainsKey(document.Id.Value))
        {
            throw new InvalidOperationException(
                $"Document '{document.Id.Value}' was not found.");
        }

        _documents[document.Id.Value] = document;

        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(
        DocumentId documentId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var removed = _documents.Remove(documentId.Value);

        return Task.FromResult(removed);
    }
}
