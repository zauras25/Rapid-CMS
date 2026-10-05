using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class RemoveAssetFromDocumentHandlerTests
{
    [Fact]
    public async Task RemoveAssetFromDocument_Removes_Asset_From_Document()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        var assetId = new AssetId(Guid.NewGuid());

        document.AddAsset(assetId);

        await documentRepository.AddAsync(document);
        assetRepository.Add(assetId);

        var handler = new RemoveAssetFromDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new RemoveAssetFromDocumentCommand(
                document.Id.Value,
                assetId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.DoesNotContain(assetId, document.AssetIds);
    }

    [Fact]
    public async Task RemoveAssetFromDocument_With_Missing_Document_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var handler = new RemoveAssetFromDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new RemoveAssetFromDocumentCommand(
                Guid.NewGuid(),
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveAssetFromDocument_With_Missing_Asset_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new RemoveAssetFromDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new RemoveAssetFromDocumentCommand(
                document.Id.Value,
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveAssetFromDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var handler = new RemoveAssetFromDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new RemoveAssetFromDocumentCommand(
                Guid.Empty,
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveAssetFromDocument_With_Empty_Asset_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new RemoveAssetFromDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new RemoveAssetFromDocumentCommand(
                document.Id.Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task RemoveAssetFromDocument_When_Cancellation_Is_Requested_Throws()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var handler = new RemoveAssetFromDocumentHandler(
            documentRepository,
            assetRepository);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new RemoveAssetFromDocumentCommand(
                    Guid.NewGuid(),
                    Guid.NewGuid()),
                cts.Token));
    }
}
