using RapidCMS.Application.Documents;
using RapidCMS.Application.Tests.Fakes;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests;

public sealed class AddPrototypeToDocumentHandlerTests
{
    [Fact]
    public async Task AddPrototypeToDocument_Adds_Prototype_To_Document()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var prototypeRepository = new InMemoryPrototypeRepository();

        var document = Document.Create(DocumentId.New());
        var prototypeId = PrototypeId.New();

        await documentRepository.AddAsync(document);
        prototypeRepository.Add(prototypeId);

        var handler = new AddPrototypeToDocumentHandler(
            documentRepository,
            prototypeRepository);

        var result = await handler.HandleAsync(
            new AddPrototypeToDocumentCommand(
                document.Id.Value,
                prototypeId.Value));

        Assert.True(result.Succeeded);
        Assert.Empty(result.Errors);
        Assert.Contains(prototypeId, document.PrototypeIds);
    }

    [Fact]
    public async Task AddPrototypeToDocument_With_Missing_Document_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var prototypeRepository = new InMemoryPrototypeRepository();

        var prototypeId = PrototypeId.New();
        prototypeRepository.Add(prototypeId);

        var handler = new AddPrototypeToDocumentHandler(
            documentRepository,
            prototypeRepository);

        var result = await handler.HandleAsync(
            new AddPrototypeToDocumentCommand(
                DocumentId.New().Value,
                prototypeId.Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPrototypeToDocument_With_Missing_Prototype_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var prototypeRepository = new InMemoryPrototypeRepository();

        var document = Document.Create(DocumentId.New());
        await documentRepository.AddAsync(document);

        var handler = new AddPrototypeToDocumentHandler(
            documentRepository,
            prototypeRepository);

        var result = await handler.HandleAsync(
            new AddPrototypeToDocumentCommand(
                document.Id.Value,
                PrototypeId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPrototypeToDocument_With_Empty_Document_Id_Returns_Failure()
    {
        var handler = new AddPrototypeToDocumentHandler(
            new InMemoryDocumentRepository(),
            new InMemoryPrototypeRepository());

        var result = await handler.HandleAsync(
            new AddPrototypeToDocumentCommand(
                Guid.Empty,
                PrototypeId.New().Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPrototypeToDocument_With_Empty_Prototype_Id_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var prototypeRepository = new InMemoryPrototypeRepository();

        var document = Document.Create(DocumentId.New());
        await documentRepository.AddAsync(document);

        var handler = new AddPrototypeToDocumentHandler(
            documentRepository,
            prototypeRepository);

        var result = await handler.HandleAsync(
            new AddPrototypeToDocumentCommand(
                document.Id.Value,
                Guid.Empty));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPrototypeToDocument_When_Prototype_Already_Attached_Returns_Failure()
    {
        var documentRepository = new InMemoryDocumentRepository();
        var prototypeRepository = new InMemoryPrototypeRepository();

        var document = Document.Create(DocumentId.New());
        var prototypeId = PrototypeId.New();

        document.AddPrototype(prototypeId);

        await documentRepository.AddAsync(document);
        prototypeRepository.Add(prototypeId);

        var handler = new AddPrototypeToDocumentHandler(
            documentRepository,
            prototypeRepository);

        var result = await handler.HandleAsync(
            new AddPrototypeToDocumentCommand(
                document.Id.Value,
                prototypeId.Value));

        Assert.False(result.Succeeded);
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public async Task AddPrototypeToDocument_When_Cancellation_Is_Requested_Throws()
    {
        var handler = new AddPrototypeToDocumentHandler(
            new InMemoryDocumentRepository(),
            new InMemoryPrototypeRepository());

        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(
            () => handler.HandleAsync(
                new AddPrototypeToDocumentCommand(
                    DocumentId.New().Value,
                    PrototypeId.New().Value),
                cts.Token));
    }
}
