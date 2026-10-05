using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Projects;

namespace RapidCMS.Domain.Tests;

public class ProjectTests
{
    [Fact]
    public void Project_Can_Be_Created()
    {
        var project = Project.Create();

        Assert.NotEqual(Guid.Empty, project.Id.Value);
        Assert.Empty(project.DocumentIds);
    }

    [Fact]
    public void Project_Can_Be_Created_With_Stable_Id()
    {
        var projectId = ProjectId.New();

        var project = Project.Create(projectId);

        Assert.Equal(projectId, project.Id);
    }

    [Fact]
    public void Project_Cannot_Be_Created_With_Empty_Id()
    {
        Assert.Throws<ArgumentException>(
            () => Project.Create(
                new ProjectId(Guid.Empty)));
    }

    [Fact]
    public void Project_Can_Add_Document()
    {
        var project = Project.Create();
        var documentId = DocumentId.New();

        project.AddDocument(documentId);

        Assert.Contains(documentId, project.DocumentIds);
    }

    [Fact]
    public void Project_Cannot_Add_Duplicate_Document()
    {
        var project = Project.Create();
        var documentId = DocumentId.New();

        project.AddDocument(documentId);

        Assert.Throws<InvalidOperationException>(
            () => project.AddDocument(documentId));
    }

    [Fact]
    public void Project_Cannot_Add_Empty_Document_Id()
    {
        var project = Project.Create();

        Assert.Throws<ArgumentException>(
            () => project.AddDocument(
                new DocumentId(Guid.Empty)));
    }

    [Fact]
    public void Project_Can_Remove_Document()
    {
        var project = Project.Create();
        var documentId = DocumentId.New();

        project.AddDocument(documentId);
        project.RemoveDocument(documentId);

        Assert.DoesNotContain(documentId, project.DocumentIds);
    }
}
