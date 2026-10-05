using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Domain.Tests;

public class PageRootNodeTests
{
    [Fact]
    public void Page_Cannot_Set_Two_Root_Nodes()
    {
        var page = Page.Create("Home");

        var firstRoot = Node.Create("Root");
        var secondRoot = Node.Create("AnotherRoot");

        page.SetRootNode(firstRoot.Id);

        Assert.Throws<InvalidOperationException>(
            () => page.SetRootNode(secondRoot.Id));

        Assert.Equal(
            firstRoot.Id,
            page.RootNodeId);
    }

    [Fact]
    public void Page_Cannot_Set_Empty_Root_Node_Id()
    {
        var page = Page.Create("Home");

        Assert.Throws<ArgumentException>(
            () => page.SetRootNode(
                new RapidCMS.Domain.Identity.NodeId(Guid.Empty)));
    }
}
