using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Tests;

public class DocumentTests
{
    [Fact]
    public void Document_Can_Be_Created_With_Project_Ownership()
    {
        var projectId = ProjectId.New();

        var document = Document.Create(projectId);

        Assert.NotEqual(Guid.Empty, document.Id.Value);
        Assert.Equal(projectId, document.ProjectId);
        Assert.Empty(document.PageIds);
    }

    [Fact]
    public void Document_Can_Be_Created_With_Stable_Id_And_Project()
    {
        var documentId = DocumentId.New();
        var projectId = ProjectId.New();

        var document = Document.Create(documentId, projectId);

        Assert.Equal(documentId, document.Id);
        Assert.Equal(projectId, document.ProjectId);
    }

    [Fact]
    public void Document_Cannot_Be_Created_With_Empty_Project_Id()
    {
        Assert.Throws<ArgumentException>(
            () => Document.Create(
                new ProjectId(Guid.Empty)));
    }

    [Fact]
    public void Document_Cannot_Be_Created_With_Empty_Document_Id()
    {
        Assert.Throws<ArgumentException>(
            () => Document.Create(
                new DocumentId(Guid.Empty),
                ProjectId.New()));
    }

    [Fact]
    public void Document_Can_Add_Page()
    {
        var document = Document.Create(ProjectId.New());
        var pageId = PageId.New();

        document.AddPage(pageId);

        Assert.Contains(pageId, document.PageIds);
    }

    [Fact]
    public void Document_Cannot_Add_Duplicate_Page()
    {
        var document = Document.Create(ProjectId.New());
        var pageId = PageId.New();

        document.AddPage(pageId);

        Assert.Throws<InvalidOperationException>(
            () => document.AddPage(pageId));
    }

    [Fact]
    public void Document_Can_Remove_Page()
    {
        var document = Document.Create(ProjectId.New());
        var pageId = PageId.New();

        document.AddPage(pageId);
        document.RemovePage(pageId);

        Assert.DoesNotContain(pageId, document.PageIds);
    }
}
