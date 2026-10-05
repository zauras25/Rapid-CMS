using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Domain.Tests;

public class DomainFoundationTests
{
    [Fact]
    public void Document_Can_Add_And_Remove_Page()
    {
        var document = Document.Create();
        var page = Page.Create("Home");

        document.AddPage(page.Id);

        Assert.Contains(page.Id, document.PageIds);

        document.RemovePage(page.Id);

        Assert.DoesNotContain(page.Id, document.PageIds);
    }

    [Fact]
    public void Page_Can_Set_Root_Node()
    {
        var page = Page.Create("Home");
        var root = Node.Create("Root");

        page.SetRootNode(root.Id);

        Assert.Equal(root.Id, page.RootNodeId);
    }

    [Fact]
    public void Node_Can_Become_Child_Of_Another_Node()
    {
        var parent = Node.Create("Parent");
        var child = Node.Create("Child");

        parent.AddChild(child);

        Assert.Contains(child.Id, parent.Children);
        Assert.Equal(parent.Id, child.ParentId);

        parent.RemoveChild(child);

        Assert.DoesNotContain(child.Id, parent.Children);
        Assert.Null(child.ParentId);
    }

    [Fact]
    public void Node_Cannot_Be_Child_Of_Itself()
    {
        var node = Node.Create("Node");

        Assert.Throws<InvalidOperationException>(
            () => node.AddChild(node));
    }

    [Fact]
    public void Node_Cannot_Have_Duplicate_Child()
    {
        var parent = Node.Create("Parent");
        var child = Node.Create("Child");

        parent.AddChild(child);

        Assert.Throws<InvalidOperationException>(
            () => parent.AddChild(child));
    }

    [Fact]
    public void Node_Cannot_Create_Direct_Cycle()
    {
        var parent = Node.Create("Parent");
        var child = Node.Create("Child");

        parent.AddChild(child);

        Assert.Throws<InvalidOperationException>(
            () => child.AddChild(parent));
    }

    [Fact]
    public void Node_Cannot_Create_Indirect_Cycle()
    {
        var first = Node.Create("First");
        var second = Node.Create("Second");
        var third = Node.Create("Third");

        first.AddChild(second);
        second.AddChild(third);

        Assert.Throws<InvalidOperationException>(
            () => third.AddChild(first));
    }
}
