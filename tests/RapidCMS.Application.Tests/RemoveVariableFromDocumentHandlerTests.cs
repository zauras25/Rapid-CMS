using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class RemoveVariableFromDocumentHandlerTests
{
    [Fact]
    public async Task RemoveVariableFromDocument_Removes_Variable_From_Document()
    {
        var repository = new InMemoryDocumentRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        var variableId = VariableId.New();

        document.AddVariable(variableId);
        await repository.AddAsync(document);

        var handler = new RemoveVariableFromDocumentHandler(repository);

        var result = await handler.HandleAsync(
            new RemoveVariableFromDocumentCommand(
                document.Id.Value,
                variableId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.DoesNotContain(variableId, document.VariableIds);
    }

    [Fact]
    public async Task RemoveVariableFromDocument_With_Missing_Document_Returns_Failure()
    {
        var handler = new RemoveVariableFromDocumentHandler(
            new InMemoryDocumentRepository());

        var result = await handler.HandleAsync(
            new RemoveVariableFromDocumentCommand(
                Guid.NewGuid(),
                VariableId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveVariableFromDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var handler = new RemoveVariableFromDocumentHandler(
            new InMemoryDocumentRepository());

        var result = await handler.HandleAsync(
            new RemoveVariableFromDocumentCommand(
                Guid.Empty,
                VariableId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveVariableFromDocument_With_Empty_Variable_Id_Returns_Failure()
    {
        var handler = new RemoveVariableFromDocumentHandler(
            new InMemoryDocumentRepository());

        var result = await handler.HandleAsync(
            new RemoveVariableFromDocumentCommand(
                Guid.NewGuid(),
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveVariableFromDocument_When_Cancellation_Is_Requested_Throws()
    {
        var handler = new RemoveVariableFromDocumentHandler(
            new InMemoryDocumentRepository());

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new RemoveVariableFromDocumentCommand(
                    Guid.NewGuid(),
                    VariableId.New().Value),
                cts.Token));
    }
}
