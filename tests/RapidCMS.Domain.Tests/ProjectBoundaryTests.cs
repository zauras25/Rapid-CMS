using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Projects;

namespace RapidCMS.Domain.Tests;

public class ProjectBoundaryTests
{
    [Fact]
    public void Document_Belongs_To_Explicit_Project()
    {
        var project = Project.Create();

        var document = Document.Create(project.Id);

        Assert.Equal(project.Id, document.ProjectId);
    }

    [Fact]
    public void Project_Can_Track_Its_Document()
    {
        var project = Project.Create();
        var document = Document.Create(project.Id);

        project.AddDocument(document.Id);

        Assert.True(
            project.ContainsDocument(document.Id));

        Assert.Contains(
            document.Id,
            project.DocumentIds);
    }

    [Fact]
    public void Project_Rejects_Duplicate_Document()
    {
        var project = Project.Create();
        var document = Document.Create(project.Id);

        project.AddDocument(document.Id);

        Assert.Throws<InvalidOperationException>(
            () => project.AddDocument(document.Id));
    }

    [Fact]
    public void Document_Preserves_Project_Identity()
    {
        var projectId = ProjectId.New();
        var documentId = DocumentId.New();

        var document =
            Document.Create(documentId, projectId);

        Assert.Equal(documentId, document.Id);
        Assert.Equal(projectId, document.ProjectId);
    }

    [Fact]
    public void Document_Rejects_Empty_Project_Id()
    {
        Assert.Throws<ArgumentException>(
            () => Document.Create(
                DocumentId.New(),
                new ProjectId(Guid.Empty)));
    }
}
