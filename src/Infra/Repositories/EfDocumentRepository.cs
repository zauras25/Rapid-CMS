using Microsoft.EntityFrameworkCore;
using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Infrastructure.Persistence;

namespace RapidCMS.Infrastructure.Repositories;

public sealed class EfDocumentRepository : IDocumentRepository
{
    private readonly RapidCmsDbContext _dbContext;

    public EfDocumentRepository(RapidCmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(
        Document document,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var record = ToRecord(document);

        await _dbContext.Documents.AddAsync(
            record,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);
    }

    public async Task<Document?> GetByIdAsync(
        DocumentId id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var record = await _dbContext.Documents
            .AsNoTracking()
            .Include(x => x.Pages)
            .Include(x => x.Assets)
            .Include(x => x.Variables)
            .Include(x => x.Prototypes)
            .Include(x => x.References)
            .Include(x => x.Components)
            .SingleOrDefaultAsync(
                x => x.Id == id.Value,
                cancellationToken);

        return record is null
            ? null
            : ToDomain(record);
    }

    public async Task<IReadOnlyList<Document>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var records = await _dbContext.Documents
            .AsNoTracking()
            .Include(x => x.Pages)
            .Include(x => x.Assets)
            .Include(x => x.Variables)
            .Include(x => x.Prototypes)
            .Include(x => x.References)
            .Include(x => x.Components)
            .ToListAsync(cancellationToken);

        return records
            .Select(ToDomain)
            .ToArray();
    }

    public async Task UpdateAsync(
        Document document,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var record = await _dbContext.Documents
            .Include(x => x.Pages)
            .Include(x => x.Assets)
            .Include(x => x.Variables)
            .Include(x => x.Prototypes)
            .Include(x => x.References)
            .Include(x => x.Components)
            .SingleOrDefaultAsync(
                x => x.Id == document.Id.Value,
                cancellationToken);

        if (record is null)
        {
            throw new InvalidOperationException(
                $"Document '{document.Id.Value}' was not found.");
        }

        record.ProjectId = document.ProjectId.Value;

        _dbContext.DocumentPages.RemoveRange(record.Pages);
        _dbContext.DocumentAssets.RemoveRange(record.Assets);
        _dbContext.DocumentVariables.RemoveRange(record.Variables);
        _dbContext.DocumentPrototypes.RemoveRange(record.Prototypes);
        _dbContext.DocumentReferences.RemoveRange(record.References);
        _dbContext.DocumentComponents.RemoveRange(record.Components);

        record.Pages = document.PageIds
            .Select(pageId => new DocumentPageRecord
            {
                DocumentId = document.Id.Value,
                PageId = pageId.Value
            })
            .ToList();

        record.Assets = document.AssetIds
            .Select(assetId => new DocumentAssetRecord
            {
                DocumentId = document.Id.Value,
                AssetId = assetId.Value
            })
            .ToList();

        record.Variables = document.VariableIds
            .Select(variableId => new DocumentVariableRecord
            {
                DocumentId = document.Id.Value,
                VariableId = variableId.Value
            })
            .ToList();

        record.Prototypes = document.PrototypeIds
            .Select(prototypeId => new DocumentPrototypeRecord
            {
                DocumentId = document.Id.Value,
                PrototypeId = prototypeId.Value
            })
            .ToList();

        record.References = document.ReferenceIds
            .Select(referenceId => new DocumentReferenceRecord
            {
                DocumentId = document.Id.Value,
                ReferenceId = referenceId.Value
            })
            .ToList();

        record.Components = document.ComponentIds
            .Select(componentId => new DocumentComponentRecord
            {
                DocumentId = document.Id.Value,
                ComponentId = componentId.Value
            })
            .ToList();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        DocumentId documentId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var record = await _dbContext.Documents
            .SingleOrDefaultAsync(
                x => x.Id == documentId.Value,
                cancellationToken);

        if (record is null)
            return false;

        _dbContext.Documents.Remove(record);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return true;
    }

    private static DocumentRecord ToRecord(Document document)
    {
        var documentId = document.Id.Value;

        return new DocumentRecord
        {
            Id = documentId,
            ProjectId = document.ProjectId.Value,

            Pages = document.PageIds
                .Select(pageId => new DocumentPageRecord
                {
                    DocumentId = documentId,
                    PageId = pageId.Value
                })
                .ToList(),

            Assets = document.AssetIds
                .Select(assetId => new DocumentAssetRecord
                {
                    DocumentId = documentId,
                    AssetId = assetId.Value
                })
                .ToList(),

            Variables = document.VariableIds
                .Select(variableId => new DocumentVariableRecord
                {
                    DocumentId = documentId,
                    VariableId = variableId.Value
                })
                .ToList(),

            Prototypes = document.PrototypeIds
                .Select(prototypeId => new DocumentPrototypeRecord
                {
                    DocumentId = documentId,
                    PrototypeId = prototypeId.Value
                })
                .ToList(),

            References = document.ReferenceIds
                .Select(referenceId => new DocumentReferenceRecord
                {
                    DocumentId = documentId,
                    ReferenceId = referenceId.Value
                })
                .ToList(),

            Components = document.ComponentIds
                .Select(componentId => new DocumentComponentRecord
                {
                    DocumentId = documentId,
                    ComponentId = componentId.Value
                })
                .ToList()
        };
    }

    private static Document ToDomain(DocumentRecord record)
    {
        var document = Document.Create(
            new DocumentId(record.Id),
            new ProjectId(record.ProjectId));

        foreach (var page in record.Pages)
            document.AddPage(new PageId(page.PageId));

        foreach (var asset in record.Assets)
            document.AddAsset(new AssetId(asset.AssetId));

        foreach (var variable in record.Variables)
            document.AddVariable(new VariableId(variable.VariableId));

        foreach (var prototype in record.Prototypes)
            document.AddPrototype(new PrototypeId(prototype.PrototypeId));

        foreach (var reference in record.References)
            document.AddReference(new ReferenceId(reference.ReferenceId));

        foreach (var component in record.Components)
            document.AddComponent(new ComponentId(component.ComponentId));

        return document;
    }
}
