using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Pages;

namespace RapidCMS.Application.Tests;

public sealed class AddPageToDocumentHandlerTests
{
    [Fact]
    public async Task AddPageToDocument_Adds_Page_To_Document()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var document = Document.Create(new RapidCMS.Domain.Identity.DocumentId(Guid.NewGuid()));
        var page = Page.Create("Home");

        await documentRepository.AddAsync(document);
        pageRepository.Add(page);

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        var command = new AddPageToDocumentCommand(
            document.Id.Value,
            page.Id.Value);

        var result = await handler.HandleAsync(command);

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.Contains(page.Id, document.PageIds);
    }

    [Fact]
    public async Task AddPageToDocument_With_Missing_Document_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var page = Page.Create("Home");
        pageRepository.Add(page);

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        var command = new AddPageToDocumentCommand(
            Guid.NewGuid(),
            page.Id.Value);

        var result = await handler.HandleAsync(command);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPageToDocument_With_Missing_Page_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var document = Document.Create(new RapidCMS.Domain.Identity.DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        var command = new AddPageToDocumentCommand(
            document.Id.Value,
            Guid.NewGuid());

        var result = await handler.HandleAsync(command);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
        Assert.Empty(document.PageIds);
    }

    [Fact]
    public async Task AddPageToDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        var command = new AddPageToDocumentCommand(
            Guid.Empty,
            Guid.NewGuid());

        var result = await handler.HandleAsync(command);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPageToDocument_With_Empty_Page_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var document = Document.Create(new RapidCMS.Domain.Identity.DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        var command = new AddPageToDocumentCommand(
            document.Id.Value,
            Guid.Empty);

        var result = await handler.HandleAsync(command);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPageToDocument_Cannot_Add_Duplicate_Page()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var document = Document.Create(new RapidCMS.Domain.Identity.DocumentId(Guid.NewGuid()));
        var page = Page.Create("Home");

        await documentRepository.AddAsync(document);
        pageRepository.Add(page);

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        var command = new AddPageToDocumentCommand(
            document.Id.Value,
            page.Id.Value);

        var first = await handler.HandleAsync(command);
        var second = await handler.HandleAsync(command);

        Assert.True(first.Succeeded);
        Assert.False(second.Succeeded);
        Assert.NotEmpty(second.Errors);
        Assert.Single(document.PageIds);
    }

    [Fact]
    public async Task AddPageToDocument_When_Cancellation_Is_Requested_Throws()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var pageRepository = new InMemoryPageRepository();

        var handler = new AddPageToDocumentHandler(
            documentRepository,
            pageRepository);

        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        var command = new AddPageToDocumentCommand(
            Guid.NewGuid(),
            Guid.NewGuid());

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                command,
                cancellationTokenSource.Token));
    }
}



