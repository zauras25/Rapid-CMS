using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class AddComponentToDocumentHandlerTests
{
    [Fact]
    public async Task AddComponentToDocument_Adds_Component_To_Document()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();

        var document = Document.Create(DocumentId.New());
        var componentId = ComponentId.New();

        await documentRepository.AddAsync(document);
        componentRepository.Add(componentId);

        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        var result = await handler.HandleAsync(
            new AddComponentToDocumentCommand(
                document.Id.Value,
                componentId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.Contains(componentId, document.ComponentIds);
    }

    [Fact]
    public async Task AddComponentToDocument_With_Missing_Document_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();

        var componentId = ComponentId.New();
        componentRepository.Add(componentId);

        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        var result = await handler.HandleAsync(
            new AddComponentToDocumentCommand(
                DocumentId.New().Value,
                componentId.Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddComponentToDocument_With_Missing_Component_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();

        var document = Document.Create(DocumentId.New());
        await documentRepository.AddAsync(document);

        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        var result = await handler.HandleAsync(
            new AddComponentToDocumentCommand(
                document.Id.Value,
                ComponentId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddComponentToDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();

        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        var result = await handler.HandleAsync(
            new AddComponentToDocumentCommand(
                Guid.Empty,
                ComponentId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddComponentToDocument_With_Empty_Component_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();

        var document = Document.Create(DocumentId.New());
        await documentRepository.AddAsync(document);

        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        var result = await handler.HandleAsync(
            new AddComponentToDocumentCommand(
                document.Id.Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddComponentToDocument_When_Component_Already_Attached_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();

        var document = Document.Create(DocumentId.New());
        var componentId = ComponentId.New();

        document.AddComponent(componentId);

        await documentRepository.AddAsync(document);
        componentRepository.Add(componentId);

        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        var result = await handler.HandleAsync(
            new AddComponentToDocumentCommand(
                document.Id.Value,
                componentId.Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddComponentToDocument_When_Cancellation_Is_Requested_Throws()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var componentRepository = new InMemoryComponentRepository();
        var handler = new AddComponentToDocumentHandler(
            documentRepository,
            componentRepository);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new AddComponentToDocumentCommand(
                    ComponentId.New().Value,
                    ComponentId.New().Value),
                cts.Token));
    }
}
