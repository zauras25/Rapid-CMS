using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Pages;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeDocumentTests
{
    [Fact]
    public void RuntimeDocument_Can_Be_Created_From_Domain_Document()
    {
        var document = Document.Create();

        var runtimeDocument = RuntimeDocument.From(
            document,
            new Dictionary<PageId, RuntimePage>());

        Assert.Equal(document.Id, runtimeDocument.Id);
        Assert.Empty(runtimeDocument.Pages);
    }

    [Fact]
    public void RuntimeDocument_Copies_Document_Pages()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        var nodes = new Dictionary<NodeId, Node>();

        var runtimePage = RuntimePage.From(page, nodes);

        var pages = new Dictionary<PageId, RuntimePage>
        {
            [page.Id] = runtimePage
        };

        var runtimeDocument = RuntimeDocument.From(document, pages);

        Assert.Single(runtimeDocument.Pages);
        Assert.Equal(page.Id, runtimeDocument.Pages[0].Id);
        Assert.Equal("Home", runtimeDocument.Pages[0].Name);
    }

    [Fact]
    public void RuntimeDocument_Can_Hold_Runtime_Pages()
    {
        var document = Document.Create();

        var page = Page.Create("Home");
        document.AddPage(page.Id);

        var runtimePage = RuntimePage.From(
            page,
            new Dictionary<NodeId, Node>());

        var pages = new Dictionary<PageId, RuntimePage>
        {
            [page.Id] = runtimePage
        };

        var runtimeDocument = RuntimeDocument.From(document, pages);

        Assert.Single(runtimeDocument.Pages);
        Assert.Equal(page.Id, runtimeDocument.Pages[0].Id);
    }

    [Fact]
    public void RuntimeDocument_Preserves_Page_Order()
    {
        var document = Document.Create();

        var home = Page.Create("Home");
        var about = Page.Create("About");

        document.AddPage(home.Id);
        document.AddPage(about.Id);

        var nodes = new Dictionary<NodeId, Node>();

        var pages = new Dictionary<PageId, RuntimePage>
        {
            [home.Id] = RuntimePage.From(home, nodes),
            [about.Id] = RuntimePage.From(about, nodes)
        };

        var runtimeDocument = RuntimeDocument.From(document, pages);

        Assert.Equal(2, runtimeDocument.Pages.Count);

        Assert.Equal(home.Id, runtimeDocument.Pages[0].Id);
        Assert.Equal(about.Id, runtimeDocument.Pages[1].Id);

        Assert.Equal("Home", runtimeDocument.Pages[0].Name);
        Assert.Equal("About", runtimeDocument.Pages[1].Name);
    }
}
