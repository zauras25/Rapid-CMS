using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Application.Abstractions;

public interface INodeRepository
{
    Task<Node?> GetByIdAsync(
        NodeId nodeId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Node>> GetByPageIdAsync(
        PageId pageId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Node node,
        PageId pageId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Node node,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        NodeId nodeId,
        CancellationToken cancellationToken = default);
}
