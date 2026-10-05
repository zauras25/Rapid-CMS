using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Pages;

public sealed class Page : Entity<PageId>
{
    public DocumentId DocumentId { get; }

    public string Name { get; private set; }

    public NodeId? RootNodeId { get; private set; }

    private Page(
        PageId id,
        DocumentId documentId,
        string name)
        : base(id)
    {
        if (documentId.Value == Guid.Empty)
            throw new ArgumentException(
                "Document ID cannot be empty.",
                nameof(documentId));

        DocumentId = documentId;
        Name = name;
    }

    public static Page Create(
        DocumentId documentId,
        string name)
    {
        if (documentId.Value == Guid.Empty)
            throw new ArgumentException(
                "Document ID cannot be empty.",
                nameof(documentId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Page name cannot be empty.",
                nameof(name));

        return new Page(
            PageId.New(),
            documentId,
            name);
    }

    public static Page Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Page name cannot be empty.",
                nameof(name));

        return new Page(
            PageId.New(),
            DocumentId.New(),
            name);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Page name cannot be empty.",
                nameof(name));

        Name = name;
    }

    public void SetRootNode(NodeId rootNodeId)
    {
        if (rootNodeId.Value == Guid.Empty)
            throw new ArgumentException(
                "Root node ID cannot be empty.",
                nameof(rootNodeId));

        if (RootNodeId.HasValue)
            throw new InvalidOperationException(
                "Page already has a root node.");

        RootNodeId = rootNodeId;
    }
}
