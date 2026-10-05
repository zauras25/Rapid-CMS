using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class RemoveAssetFromDocumentHandler
    : ICommandHandler<RemoveAssetFromDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IAssetRepository _assetRepository;

    public RemoveAssetFromDocumentHandler(
        IDocumentRepository documentRepository,
        IAssetRepository assetRepository)
    {
        _documentRepository = documentRepository;
        _assetRepository = assetRepository;
    }

    public async Task<CommandResult> HandleAsync(
        RemoveAssetFromDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult.Failure(
                "Document ID cannot be empty.");

        if (command.AssetId == Guid.Empty)
            return CommandResult.Failure(
                "Asset ID cannot be empty.");

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(command.DocumentId),
            cancellationToken);

        if (document is null)
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");

        var assetExists = await _assetRepository.ExistsAsync(
            new AssetId(command.AssetId),
            cancellationToken);

        if (!assetExists)
            return CommandResult.Failure(
                $"Asset '{command.AssetId}' was not found.");

        try
        {
            document.RemoveAsset(new AssetId(command.AssetId));
        }
        catch (InvalidOperationException exception)
        {
            return CommandResult.Failure(exception.Message);
        }

        return CommandResult.Success();
    }
}
