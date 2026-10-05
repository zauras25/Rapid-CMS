using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;

namespace RapidCMS.Application.Tests;

public sealed class DeleteDocumentHandlerTests
{
    [Fact]
    public async Task DeleteDocument_Removes_Document()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create();

        await repository.AddAsync(document);

        var handler = new DeleteDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new DeleteDocumentCommand(document.Id.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);

        var storedDocument = await repository.GetByIdAsync(
            document.Id);

        Assert.Null(storedDocument);
    }

    [Fact]
    public async Task DeleteDocument_Returns_Failure_When_Document_Does_Not_Exist()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new DeleteDocumentHandler(repository);

        var documentId = Guid.NewGuid();

        var result = await handler.HandleAsync(
            new DeleteDocumentCommand(documentId));

        Assert.False(result.Succeeded);
        Assert.Single(result.Errors);
        Assert.Equal(
            $"Document '{documentId}' was not found.",
            result.Errors[0]);
    }

    [Fact]
    public async Task DeleteDocument_Rejects_Empty_Id()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new DeleteDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new DeleteDocumentCommand(Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.Single(result.Errors);
        Assert.Equal(
            "Document ID cannot be empty.",
            result.Errors[0]);
    }

    [Fact]
    public async Task DeleteDocument_Honors_Cancellation()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new DeleteDocumentHandler(repository);

        using var cancellationTokenSource =
            new CancellationTokenSource();

        cancellationTokenSource.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new DeleteDocumentCommand(Guid.NewGuid()),
                cancellationTokenSource.Token));
    }
}
