using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class RemovePrototypeFromDocumentHandlerTests
{
    [Fact]
    public async Task RemovePrototypeFromDocument_Removes_Prototype_From_Document()
    {
        var repository = new InMemoryDocumentRepository();

        var document = Document.Create(DocumentId.New());
        var prototypeId = PrototypeId.New();

        document.AddPrototype(prototypeId);
        await repository.AddAsync(document);

        var handler = new RemovePrototypeFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemovePrototypeFromDocumentCommand(
                document.Id.Value,
                prototypeId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.DoesNotContain(prototypeId, document.PrototypeIds);
    }

    [Fact]
    public async Task RemovePrototypeFromDocument_With_Missing_Document_Returns_Failure()
    {
        var handler = new RemovePrototypeFromDocumentHandler(
            new InMemoryDocumentRepository());

        var result = await handler.HandleAsync(
            new RemovePrototypeFromDocumentCommand(
                DocumentId.New().Value,
                PrototypeId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemovePrototypeFromDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var handler = new RemovePrototypeFromDocumentHandler(
            new InMemoryDocumentRepository());

        var result = await handler.HandleAsync(
            new RemovePrototypeFromDocumentCommand(
                Guid.Empty,
                PrototypeId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemovePrototypeFromDocument_With_Empty_Prototype_Id_Returns_Failure()
    {
        var handler = new RemovePrototypeFromDocumentHandler(
            new InMemoryDocumentRepository());

        var result = await handler.HandleAsync(
            new RemovePrototypeFromDocumentCommand(
                DocumentId.New().Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemovePrototypeFromDocument_When_Cancellation_Is_Requested_Throws()
    {
        var handler = new RemovePrototypeFromDocumentHandler(
            new InMemoryDocumentRepository());

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new RemovePrototypeFromDocumentCommand(
                    DocumentId.New().Value,
                    PrototypeId.New().Value),
                cts.Token));
    }
}
