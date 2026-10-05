using RapidCMS.Application.Documents;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Application.Tests.Fakes;

namespace RapidCMS.Application.Tests;

public sealed class GetDocumentByIdHandlerTests
{
    [Fact]
    public async Task GetDocumentById_Returns_Document()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create();

        await repository.AddAsync(document);

        var handler = new GetDocumentByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetDocumentByIdQuery(document.Id.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
        Assert.Equal(document.Id.Value, result.Data!.Id);
    }

    [Fact]
    public async Task GetDocumentById_Maps_All_Document_Relationships()
    {
        var repository = new InMemoryDocumentRepository();
        var document = Document.Create();

        var pageId = PageId.New();
        var assetId = AssetId.New();
        var variableId = VariableId.New();
        var prototypeId = PrototypeId.New();
        var referenceId = ReferenceId.New();
        var componentId = ComponentId.New();

        document.AddPage(pageId);
        document.AddAsset(assetId);
        document.AddVariable(variableId);
        document.AddPrototype(prototypeId);
        document.AddReference(referenceId);
        document.AddComponent(componentId);

        await repository.AddAsync(document);

        var handler = new GetDocumentByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetDocumentByIdQuery(document.Id.Value));

        Assert.True(result.Succeeded);
        Assert.NotNull(result.Data);

        Assert.Equal(
            document.Id.Value,
            result.Data!.Id);

        Assert.Equal(
            new[] { pageId.Value },
            result.Data.PageIds);

        Assert.Equal(
            new[] { assetId.Value },
            result.Data.AssetIds);

        Assert.Equal(
            new[] { variableId.Value },
            result.Data.VariableIds);

        Assert.Equal(
            new[] { prototypeId.Value },
            result.Data.PrototypeIds);

        Assert.Equal(
            new[] { referenceId.Value },
            result.Data.ReferenceIds);

        Assert.Equal(
            new[] { componentId.Value },
            result.Data.ComponentIds);
    }

    [Fact]
    public async Task GetDocumentById_Returns_Failure_When_Document_Does_Not_Exist()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new GetDocumentByIdHandler(repository);

        var documentId = Guid.NewGuid();

        var result = await handler.HandleAsync(
            new GetDocumentByIdQuery(documentId));

        Assert.False(result.Succeeded);
        Assert.Null(result.Data);
        Assert.Single(result.Errors);
        Assert.Equal(
            $"Document '{documentId}' was not found.",
            result.Errors[0]);
    }

    [Fact]
    public async Task GetDocumentById_Rejects_Empty_Id()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new GetDocumentByIdHandler(repository);

        var result = await handler.HandleAsync(
            new GetDocumentByIdQuery(Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.Null(result.Data);
        Assert.Single(result.Errors);
        Assert.Equal(
            "Document ID cannot be empty.",
            result.Errors[0]);
    }

    [Fact]
    public async Task GetDocumentById_Honors_Cancellation()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new GetDocumentByIdHandler(repository);

        using var cancellationTokenSource =
            new CancellationTokenSource();

        cancellationTokenSource.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new GetDocumentByIdQuery(Guid.NewGuid()),
                cancellationTokenSource.Token));
    }
}
