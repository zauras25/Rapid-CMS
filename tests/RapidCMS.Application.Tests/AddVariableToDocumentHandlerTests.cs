using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class AddVariableToDocumentHandlerTests
{
    [Fact]
    public async Task AddVariableToDocument_Adds_Variable_To_Document()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var variableRepository = new InMemoryVariableRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        var variableId = VariableId.New();

        variableRepository.Add(variableId);
        await documentRepository.AddAsync(document);

        var handler = new AddVariableToDocumentHandler(
            documentRepository,
            variableRepository);

        var result = await handler.HandleAsync(
            new AddVariableToDocumentCommand(
                document.Id.Value,
                variableId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.Contains(variableId, document.VariableIds);
    }

    [Fact]
    public async Task AddVariableToDocument_With_Missing_Document_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var variableRepository = new InMemoryVariableRepository();

        var handler = new AddVariableToDocumentHandler(
            documentRepository,
            variableRepository);

        var result = await handler.HandleAsync(
            new AddVariableToDocumentCommand(
                Guid.NewGuid(),
                VariableId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddVariableToDocument_With_Missing_Variable_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var variableRepository = new InMemoryVariableRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new AddVariableToDocumentHandler(
            documentRepository,
            variableRepository);

        var result = await handler.HandleAsync(
            new AddVariableToDocumentCommand(
                document.Id.Value,
                VariableId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddVariableToDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var handler = new AddVariableToDocumentHandler(
            new InMemoryDocumentRepository(),
            new InMemoryVariableRepository());

        var result = await handler.HandleAsync(
            new AddVariableToDocumentCommand(
                Guid.Empty,
                VariableId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddVariableToDocument_With_Empty_Variable_Id_Returns_Failure()
    {
        var handler = new AddVariableToDocumentHandler(
            new InMemoryDocumentRepository(),
            new InMemoryVariableRepository());

        var result = await handler.HandleAsync(
            new AddVariableToDocumentCommand(
                Guid.NewGuid(),
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddVariableToDocument_When_Cancellation_Is_Requested_Throws()
    {
        var handler = new AddVariableToDocumentHandler(
            new InMemoryDocumentRepository(),
            new InMemoryVariableRepository());

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new AddVariableToDocumentCommand(
                    Guid.NewGuid(),
                    VariableId.New().Value),
                cts.Token));
    }
}
