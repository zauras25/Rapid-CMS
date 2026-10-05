using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Pages;

public sealed class Page : Entity<PageId>
{
    public string Name { get; private set; }

    public NodeId? RootNodeId { get; private set; }

    private Page(PageId id, string name)
        : base(id)
    {
        Name = name;
    }

    public static Page Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Page name cannot be empty.",
                nameof(name));

        return new Page(PageId.New(), name);
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
