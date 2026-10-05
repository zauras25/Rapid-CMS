using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class GetDocumentsHandlerTests
{
    [Fact]
    public async Task GetDocuments_Returns_All_Documents()
    {
        var repository = new InMemoryDocumentRepository();

        var first = Document.Create();
        var second = Document.Create();

        await repository.AddAsync(first);
        await repository.AddAsync(second);

        var handler = new GetDocumentsHandler(repository);

        var result = await handler.HandleAsync(
            new GetDocumentsQuery());

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data!.Count);
        Assert.Contains(
            result.Data,
            document => document.Id == first.Id.Value);
        Assert.Contains(
            result.Data,
            document => document.Id == second.Id.Value);
    }

    [Fact]
    public async Task GetDocuments_Returns_Empty_List_When_No_Documents_Exist()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new GetDocumentsHandler(repository);

        var result = await handler.HandleAsync(
            new GetDocumentsQuery());

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data!);
    }

    [Fact]
    public async Task GetDocuments_Maps_Document_Relationship_Ids()
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

        var handler = new GetDocumentsHandler(repository);

        var result = await handler.HandleAsync(
            new GetDocumentsQuery());

        Assert.True(result.Succeeded);
        Assert.NotNull(result.Data);

        var dto = Assert.Single(result.Data!);

        Assert.Equal(document.Id.Value, dto.Id);
        Assert.Contains(pageId.Value, dto.PageIds);
        Assert.Contains(assetId.Value, dto.AssetIds);
        Assert.Contains(variableId.Value, dto.VariableIds);
        Assert.Contains(prototypeId.Value, dto.PrototypeIds);
        Assert.Contains(referenceId.Value, dto.ReferenceIds);
        Assert.Contains(componentId.Value, dto.ComponentIds);
    }

    [Fact]
    public async Task GetDocuments_Honors_Cancellation()
    {
        var repository = new InMemoryDocumentRepository();
        var handler = new GetDocumentsHandler(repository);

        using var cancellationTokenSource =
            new CancellationTokenSource();

        cancellationTokenSource.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new GetDocumentsQuery(),
                cancellationTokenSource.Token));
    }
}
