using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Documents;

public sealed partial class Document : Entity<DocumentId>
{
    private readonly List<PageId> _pageIds = new();
    private readonly List<AssetId> _assetIds = new();
    private readonly List<VariableId> _variableIds = new();
    private readonly List<PrototypeId> _prototypeIds = new();
    private readonly List<ReferenceId> _referenceIds = new();
    private readonly List<ComponentId> _componentIds = new();

    public ProjectId ProjectId { get; }

    public IReadOnlyList<PageId> PageIds => _pageIds;
    public IReadOnlyList<AssetId> AssetIds => _assetIds;
    public IReadOnlyList<VariableId> VariableIds => _variableIds;
    public IReadOnlyList<PrototypeId> PrototypeIds => _prototypeIds;
    public IReadOnlyList<ReferenceId> ReferenceIds => _referenceIds;
    public IReadOnlyList<ComponentId> ComponentIds => _componentIds;

    private Document(
        DocumentId id,
        ProjectId projectId)
        : base(id)
    {
        if (projectId.Value == Guid.Empty)
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(projectId));

        ProjectId = projectId;
    }

    public static Document Create()
    {
        return new Document(
            DocumentId.New(),
            ProjectId.New());
    }

    public static Document Create(ProjectId projectId)
    {
        if (projectId.Value == Guid.Empty)
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(projectId));

        return new Document(
            DocumentId.New(),
            projectId);
    }

    public static Document Create(DocumentId id, ProjectId projectId)
    {
        if (id.Value == Guid.Empty)
            throw new ArgumentException(
                "Document ID cannot be empty.",
                nameof(id));

        if (projectId.Value == Guid.Empty)
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(projectId));

        return new Document(id, projectId);
    }


    public static Document Create(DocumentId id)
    {
        if (id.Value == Guid.Empty)
            throw new ArgumentException(
                "Document ID cannot be empty.",
                nameof(id));

        return new Document(id, ProjectId.New());
    }

    public void AddPage(PageId pageId)
    {
        if (pageId.Value == Guid.Empty)
            throw new ArgumentException("Page ID cannot be empty.", nameof(pageId));

        if (_pageIds.Contains(pageId))
            throw new InvalidOperationException(
                $"Page '{pageId}' is already part of the document.");

        _pageIds.Add(pageId);
    }

    public void RemovePage(PageId pageId)
    {
        _pageIds.Remove(pageId);
    }

    public void AddAsset(AssetId assetId)
    {
        if (assetId.Value == Guid.Empty)
            throw new ArgumentException("Asset ID cannot be empty.", nameof(assetId));

        if (_assetIds.Contains(assetId))
            throw new InvalidOperationException(
                $"Asset '{assetId}' is already part of the document.");

        _assetIds.Add(assetId);
    }

    public void RemoveAsset(AssetId assetId)
    {
        _assetIds.Remove(assetId);
    }

    public void AddVariable(VariableId variableId)
    {
        if (variableId.Value == Guid.Empty)
            throw new ArgumentException("Variable ID cannot be empty.", nameof(variableId));

        if (_variableIds.Contains(variableId))
            throw new InvalidOperationException(
                $"Variable '{variableId}' is already part of the document.");

        _variableIds.Add(variableId);
    }

    public void RemoveVariable(VariableId variableId)
    {
        _variableIds.Remove(variableId);
    }

    public void AddPrototype(PrototypeId prototypeId)
    {
        if (prototypeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Prototype ID cannot be empty.",
                nameof(prototypeId));

        if (_prototypeIds.Contains(prototypeId))
            throw new InvalidOperationException(
                $"Prototype '{prototypeId}' is already part of the document.");

        _prototypeIds.Add(prototypeId);
    }

    public void RemovePrototype(PrototypeId prototypeId)
    {
        _prototypeIds.Remove(prototypeId);
    }

    public void AddReference(ReferenceId referenceId)
    {
        if (referenceId.Value == Guid.Empty)
            throw new ArgumentException(
                "Reference ID cannot be empty.",
                nameof(referenceId));

        if (_referenceIds.Contains(referenceId))
            throw new InvalidOperationException(
                $"Reference '{referenceId}' is already part of the document.");

        _referenceIds.Add(referenceId);
    }

    public void RemoveReference(ReferenceId referenceId)
    {
        _referenceIds.Remove(referenceId);
    }


}

