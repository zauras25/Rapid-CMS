using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Domain.Tests;

public class PageTests
{
    [Fact]
    public void Page_Can_Be_Created_With_Document_Ownership()
    {
        var documentId = DocumentId.New();

        var page = Page.Create(documentId, "Home");

        Assert.NotEqual(Guid.Empty, page.Id.Value);
        Assert.Equal(documentId, page.DocumentId);
        Assert.Equal("Home", page.Name);
        Assert.Null(page.RootNodeId);
    }

    [Fact]
    public void Page_Can_Be_Renamed()
    {
        var page = Page.Create(DocumentId.New(), "Home");

        page.Rename("Dashboard");

        Assert.Equal("Dashboard", page.Name);
    }

    [Fact]
    public void Page_Cannot_Be_Created_With_Empty_Document_Id()
    {
        Assert.Throws<ArgumentException>(
            () => Page.Create(
                new DocumentId(Guid.Empty),
                "Home"));
    }

    [Fact]
    public void Page_Cannot_Be_Created_With_Empty_Name()
    {
        Assert.Throws<ArgumentException>(
            () => Page.Create(
                DocumentId.New(),
                ""));
    }

    [Fact]
    public void Page_Can_Set_Root_Node_Only_Once()
    {
        var page = Page.Create(DocumentId.New(), "Home");
        var firstRoot = NodeId.New();
        var secondRoot = NodeId.New();

        page.SetRootNode(firstRoot);

        Assert.Equal(firstRoot, page.RootNodeId);

        Assert.Throws<InvalidOperationException>(
            () => page.SetRootNode(secondRoot));
    }

    [Fact]
    public void Page_Cannot_Set_Empty_Root_Node_Id()
    {
        var page = Page.Create(DocumentId.New(), "Home");

        Assert.Throws<ArgumentException>(
            () => page.SetRootNode(
                new NodeId(Guid.Empty)));
    }
}
