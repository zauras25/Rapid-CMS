using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class RemoveComponentFromDocumentHandlerTests
{
    [Fact]
    public async Task RemoveComponentFromDocument_Removes_Component_From_Document()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        var componentId = ComponentId.New();

        document.AddComponent(componentId);
        await repository.AddAsync(document);

        var handler = new RemoveComponentFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemoveComponentFromDocumentCommand(
                document.Id.Value,
                componentId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.DoesNotContain(
            componentId,
            document.ComponentIds);
    }

    [Fact]
    public async Task RemoveComponentFromDocument_With_Missing_Document_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new RemoveComponentFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemoveComponentFromDocumentCommand(
                Guid.NewGuid(),
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveComponentFromDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new RemoveComponentFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemoveComponentFromDocumentCommand(
                Guid.Empty,
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveComponentFromDocument_With_Empty_Component_Id_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        await repository.AddAsync(document);

        var handler = new RemoveComponentFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemoveComponentFromDocumentCommand(
                document.Id.Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveComponentFromDocument_When_Cancellation_Is_Requested_Throws()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new RemoveComponentFromDocumentHandler(repository);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new RemoveComponentFromDocumentCommand(
                    Guid.NewGuid(),
                    Guid.NewGuid()),
                cts.Token));
    }
}
