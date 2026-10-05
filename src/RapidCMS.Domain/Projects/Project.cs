using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Projects;

public sealed class Project : Entity<ProjectId>
{
    private readonly List<DocumentId> _documentIds = new();

    public IReadOnlyList<DocumentId> DocumentIds => _documentIds;

    private Project(ProjectId id)
        : base(id)
    {
    }

    public static Project Create()
    {
        return new Project(ProjectId.New());
    }

    public static Project Create(ProjectId id)
    {
        if (id.Value == Guid.Empty)
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(id));

        return new Project(id);
    }

    public void AddDocument(DocumentId documentId)
    {
        if (documentId.Value == Guid.Empty)
            throw new ArgumentException(
                "Document ID cannot be empty.",
                nameof(documentId));

        if (_documentIds.Contains(documentId))
            throw new InvalidOperationException(
                $"Document '{documentId}' is already part of the project.");

        _documentIds.Add(documentId);
    }

    public void RemoveDocument(DocumentId documentId)
    {
        _documentIds.Remove(documentId);
    }
}
