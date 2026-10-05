using RapidCMS.Domain.Components;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeDocumentBuilderTests
{
    [Fact]
    public void RuntimeDocumentBuilder_Builds_Document_With_Page()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        var pages =
            new Dictionary<RapidCMS.Domain.Identity.PageId, Page>
            {
                [page.Id] = page
            };

        var nodes =
            new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>();

        var runtimeDocument = RuntimeDocumentBuilder.Build(
            document,
            pages,
            nodes);

        Assert.Equal(document.Id, runtimeDocument.Id);
        Assert.Single(runtimeDocument.Pages);
        Assert.Equal("Home", runtimeDocument.Pages[0].Name);
    }

    [Fact]
    public void RuntimeDocumentBuilder_Builds_Page_Root_Tree()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        var root = Node.Create("Container");
        var button = Node.Create("Button");

        root.AddChild(button);
        page.SetRootNode(root.Id);

        var pages =
            new Dictionary<RapidCMS.Domain.Identity.PageId, Page>
            {
                [page.Id] = page
            };

        var nodes =
            new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>
            {
                [root.Id] = root,
                [button.Id] = button
            };

        var runtimeDocument = RuntimeDocumentBuilder.Build(
            document,
            pages,
            nodes);

        var runtimePage = runtimeDocument.Pages[0];

        Assert.NotNull(runtimePage.RootNode);
        Assert.Equal(root.Id, runtimePage.RootNode!.Id);
        Assert.Single(runtimePage.RootNode.Children);
        Assert.Equal(button.Id, runtimePage.RootNode.Children[0].Id);
    }

    [Fact]
    public void RuntimeDocumentBuilder_Copies_Component_In_Root_Tree()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        var root = Node.Create("Button");

        var component = Component.Create(
            root.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Save"));

        root.AttachComponent(component);
        page.SetRootNode(root.Id);

        var pages =
            new Dictionary<RapidCMS.Domain.Identity.PageId, Page>
            {
                [page.Id] = page
            };

        var nodes =
            new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>
            {
                [root.Id] = root
            };

        var runtimeDocument = RuntimeDocumentBuilder.Build(
            document,
            pages,
            nodes);

        var runtimeRoot =
            runtimeDocument.Pages[0].RootNode;

        Assert.NotNull(runtimeRoot);
        Assert.NotNull(runtimeRoot!.Component);
        Assert.Equal(
            "Button",
            runtimeRoot.Component!.Type);
        Assert.Equal(
            "Save",
            runtimeRoot.Component.Properties["text"]);
    }

    [Fact]
    public void RuntimeDocumentBuilder_Rejects_Missing_Page()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        Assert.Throws<InvalidOperationException>(
            () => RuntimeDocumentBuilder.Build(
                document,
                new Dictionary<RapidCMS.Domain.Identity.PageId, Page>(),
                new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>()));
    }

    [Fact]
    public void RuntimeDocumentBuilder_Rejects_Missing_Root_Node()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        var root = Node.Create("Container");
        page.SetRootNode(root.Id);

        var pages =
            new Dictionary<RapidCMS.Domain.Identity.PageId, Page>
            {
                [page.Id] = page
            };

        Assert.Throws<InvalidOperationException>(
            () => RuntimeDocumentBuilder.Build(
                document,
                pages,
                new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>()));
    }
}
