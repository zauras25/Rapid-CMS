using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class AddAssetToDocumentHandlerTests
{
    [Fact]
    public async Task AddAssetToDocument_Adds_Existing_Asset_To_Document()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        var assetId = new AssetId(Guid.NewGuid());

        await documentRepository.AddAsync(document);
        assetRepository.Add(assetId);

        var handler = new AddAssetToDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new AddAssetToDocumentCommand(
                document.Id.Value,
                assetId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.Contains(assetId, document.AssetIds);
    }

    [Fact]
    public async Task AddAssetToDocument_With_Missing_Document_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var handler = new AddAssetToDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new AddAssetToDocumentCommand(
                Guid.NewGuid(),
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddAssetToDocument_With_Missing_Asset_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new AddAssetToDocumentHandler(
            documentRepository,
            assetRepository);

        var assetId = Guid.NewGuid();

        var result = await handler.HandleAsync(
            new AddAssetToDocumentCommand(
                document.Id.Value,
                assetId));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
        Assert.DoesNotContain(
            new AssetId(assetId),
            document.AssetIds);
    }

    [Fact]
    public async Task AddAssetToDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var handler = new AddAssetToDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new AddAssetToDocumentCommand(
                Guid.Empty,
                Guid.NewGuid()));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddAssetToDocument_With_Empty_Asset_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var document = Document.Create(
            new DocumentId(Guid.NewGuid()));

        await documentRepository.AddAsync(document);

        var handler = new AddAssetToDocumentHandler(
            documentRepository,
            assetRepository);

        var result = await handler.HandleAsync(
            new AddAssetToDocumentCommand(
                document.Id.Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddAssetToDocument_When_Cancellation_Is_Requested_Throws()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var assetRepository = new InMemoryAssetRepository();

        var handler = new AddAssetToDocumentHandler(
            documentRepository,
            assetRepository);

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new AddAssetToDocumentCommand(
                    Guid.NewGuid(),
                    Guid.NewGuid()),
                cts.Token));
    }
}
