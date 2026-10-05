using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Abstractions;

public interface IDocumentRepository
{
    Task AddAsync(
        Document document,
        CancellationToken cancellationToken = default);

    Task<Document?> GetByIdAsync(
        DocumentId documentId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Document>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Document document,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        DocumentId documentId,
        CancellationToken cancellationToken = default);
}
