using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimePageTests
{
    [Fact]
    public void RuntimePage_Can_Be_Created_From_Domain_Page_Without_Root()
    {
        var page = Page.Create("Home");

        var nodes = new Dictionary<NodeId, Node>();

        var runtimePage = RuntimePage.From(
            page,
            nodes);

        Assert.Equal(page.Id, runtimePage.Id);
        Assert.Equal("Home", runtimePage.Name);
        Assert.Null(runtimePage.RootNodeId);
        Assert.Null(runtimePage.RootNode);
    }

    [Fact]
    public void RuntimePage_Copies_Root_Node()
    {
        var page = Page.Create("Home");
        var node = Node.Create("Container");

        page.SetRootNode(node.Id);

        var nodes = new Dictionary<NodeId, Node>
        {
            [node.Id] = node
        };

        var runtimePage = RuntimePage.From(
            page,
            nodes);

        Assert.Equal(node.Id, runtimePage.RootNodeId);
        Assert.NotNull(runtimePage.RootNode);
        Assert.Equal(node.Id, runtimePage.RootNode!.Id);
    }

    [Fact]
    public void RuntimePage_Rejects_Missing_Root_Node()
    {
        var page = Page.Create("Home");
        var node = Node.Create("Container");

        page.SetRootNode(node.Id);

        var nodes = new Dictionary<NodeId, Node>();

        Assert.Throws<InvalidOperationException>(
            () => RuntimePage.From(
                page,
                nodes));
    }

    [Fact]
    public void RuntimePage_Copies_Root_Tree()
    {
        var page = Page.Create("Home");

        var root = Node.Create("Container");
        var child = Node.Create("Button");

        root.AddChild(child);
        page.SetRootNode(root.Id);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root,
            [child.Id] = child
        };

        var runtimePage = RuntimePage.From(
            page,
            nodes);

        Assert.NotNull(runtimePage.RootNode);

        Assert.Equal(
            root.Id,
            runtimePage.RootNode!.Id);

        Assert.Single(
            runtimePage.RootNode.Children);

        Assert.Equal(
            child.Id,
            runtimePage.RootNode.Children[0].Id);
    }
}
