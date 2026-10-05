using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;

namespace RapidCMS.Application.Tests;

public sealed class CreateDocumentHandlerTests
{
    [Fact]
    public async Task CreateDocument_Returns_Created_Document()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new CreateDocumentHandler(repository);

        var command = new CreateDocumentCommand(Guid.NewGuid());

        var result = await handler.HandleAsync(command);

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
        Assert.NotEqual(Guid.Empty, result.Data!.Id.Value);
        Assert.Empty(result.Data.PageIds);
    }

    [Fact]
    public async Task CreateDocument_With_Empty_Id_Returns_Failure()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new CreateDocumentHandler(repository);

        var command = new CreateDocumentCommand(Guid.Empty);

        var result = await handler.HandleAsync(command);

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task CreateDocument_When_Cancellation_Is_Requested_Throws()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new CreateDocumentHandler(repository);

        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        var command = new CreateDocumentCommand(Guid.NewGuid());

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                command,
                cancellationTokenSource.Token));
    }

    [Fact]
    public async Task CreateDocument_Is_Stored_In_Repository()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new CreateDocumentHandler(repository);

        var command = new CreateDocumentCommand(Guid.NewGuid());

        var result = await handler.HandleAsync(command);

        Assert.True(result.Succeeded);
        Assert.NotNull(result.Data);

        var storedDocument = await repository.GetByIdAsync(
            result.Data!.Id);

        Assert.NotNull(storedDocument);
        Assert.Equal(result.Data.Id, storedDocument.Id);
    }

    [Fact]
    public async Task CreateDocument_With_Same_Command_Id_Does_Not_Create_Duplicate()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new CreateDocumentHandler(repository);

        var documentId = Guid.NewGuid();
        var command = new CreateDocumentCommand(documentId);

        var first = await handler.HandleAsync(command);
        var second = await handler.HandleAsync(command);

        Assert.True(first.Succeeded);
        Assert.False(second.Succeeded);
        Assert.NotEmpty(second.Errors);
    }
}
