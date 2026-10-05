using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Application.Tests;

public sealed class RemovePageFromDocumentHandlerTests
{
    [Fact]
    public async Task RemovePageFromDocument_Removes_Page_From_Document()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create(new RapidCMS.Domain.Identity.DocumentId(Guid.NewGuid()));
        var page = Page.Create("Home");

        document.AddPage(page.Id);
        await repository.AddAsync(document);

        var handler = new RemovePageFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemovePageFromDocumentCommand(
                document.Id.Value,
                page.Id.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.DoesNotContain(page.Id, document.PageIds);
    }

    [Fact]
    public async Task RemovePageFromDocument_With_Missing_Document_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new RemovePageFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemovePageFromDocumentCommand(
                Guid.NewGuid(),
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemovePageFromDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new RemovePageFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemovePageFromDocumentCommand(
                Guid.Empty,
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemovePageFromDocument_With_Empty_Page_Id_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create(new RapidCMS.Domain.Identity.DocumentId(Guid.NewGuid()));

        await repository.AddAsync(document);

        var handler = new RemovePageFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemovePageFromDocumentCommand(
                document.Id.Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemovePageFromDocument_When_Cancellation_Is_Requested_Throws()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new RemovePageFromDocumentHandler(repository);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new RemovePageFromDocumentCommand(
                    Guid.NewGuid(),
                    Guid.NewGuid()),
                cts.Token));
    }
}
