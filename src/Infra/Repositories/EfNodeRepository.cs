using Microsoft.EntityFrameworkCore;
using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Infrastructure.Persistence;

namespace RapidCMS.Infrastructure.Repositories;

public sealed class EfNodeRepository : INodeRepository
{
    private readonly RapidCmsDbContext _db;

    public EfNodeRepository(RapidCmsDbContext db)
    {
        _db = db;
    }

    public async Task<Node?> GetByIdAsync(
        NodeId nodeId,
        CancellationToken cancellationToken = default)
    {
        var record = await _db.Nodes
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.Id == nodeId.Value,
                cancellationToken);

        if (record is null)
            return null;

        var nodes = await LoadPageNodesAsync(
            record.PageId,
            cancellationToken);

        return BuildTree(nodes, record.Id)
            .FirstOrDefault(x => x.Id.Value == record.Id);
    }

    public async Task<IReadOnlyList<Node>> GetByPageIdAsync(
        PageId pageId,
        CancellationToken cancellationToken = default)
    {
        var records = await LoadPageNodesAsync(
            pageId.Value,
            cancellationToken);

        return BuildTree(records);
    }

    public async Task AddAsync(
        Node node,
        PageId pageId,
        CancellationToken cancellationToken = default)
    {
        var exists = await _db.Nodes
            .AnyAsync(
                x => x.Id == node.Id.Value,
                cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                $"Node '{node.Id}' already exists.");

        var parentId = node.ParentId?.Value;

        var maxOrder = await _db.Nodes
            .Where(x =>
                x.PageId == pageId.Value &&
                x.ParentId == parentId)
            .Select(x => (int?)x.SortOrder)
            .MaxAsync(cancellationToken) ?? -1;

        _db.Nodes.Add(new NodeRecord
        {
            Id = node.Id.Value,
            PageId = pageId.Value,
            Name = node.Name,
            ParentId = parentId,
            SortOrder = maxOrder + 1
        });

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Node node,
        CancellationToken cancellationToken = default)
    {
        var record = await _db.Nodes
            .SingleOrDefaultAsync(
                x => x.Id == node.Id.Value,
                cancellationToken);

        if (record is null)
            throw new InvalidOperationException(
                $"Node '{node.Id}' was not found.");

        record.Name = node.Name;

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        NodeId nodeId,
        CancellationToken cancellationToken = default)
    {
        var records = await _db.Nodes
            .Where(x => x.Id == nodeId.Value)
            .ToListAsync(cancellationToken);

        if (records.Count == 0)
            return false;

        var pageId = records[0].PageId;

        var all = await _db.Nodes
            .Where(x => x.PageId == pageId)
            .ToListAsync(cancellationToken);

        var idsToDelete = new HashSet<Guid>
        {
            nodeId.Value
        };

        var changed = true;

        while (changed)
        {
            changed = false;

            foreach (var record in all)
            {
                if (record.ParentId.HasValue &&
                    idsToDelete.Contains(record.ParentId.Value) &&
                    idsToDelete.Add(record.Id))
                {
                    changed = true;
                }
            }
        }

        _db.Nodes.RemoveRange(
            all.Where(x => idsToDelete.Contains(x.Id)));

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task<List<NodeRecord>> LoadPageNodesAsync(
        Guid pageId,
        CancellationToken cancellationToken)
    {
        return await _db.Nodes
            .AsNoTracking()
            .Where(x => x.PageId == pageId)
            .OrderBy(x => x.ParentId)
            .ThenBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);
    }

    private static IReadOnlyList<Node> BuildTree(
        IReadOnlyList<NodeRecord> records,
        Guid? requestedNodeId = null)
    {
        var nodes = new Dictionary<Guid, Node>();

        foreach (var record in records)
        {
            nodes[record.Id] = Node.Rehydrate(
                new NodeId(record.Id),
                record.Name);
        }

        foreach (var record in records)
        {
            if (!record.ParentId.HasValue)
                continue;

            if (!nodes.TryGetValue(
                    record.ParentId.Value,
                    out var parent))
            {
                continue;
            }

            var child = nodes[record.Id];

            parent.RestoreChild(child);
        }

        if (requestedNodeId.HasValue &&
            nodes.TryGetValue(
                requestedNodeId.Value,
                out var requested))
        {
            return new[] { requested };
        }

        return records
            .Where(x => !x.ParentId.HasValue)
            .OrderBy(x => x.SortOrder)
            .Select(x => nodes[x.Id])
            .ToArray();
    }
}
