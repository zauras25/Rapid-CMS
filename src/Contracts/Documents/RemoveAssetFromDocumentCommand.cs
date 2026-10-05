using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record RemoveAssetFromDocumentCommand(
    Guid DocumentId,
    Guid AssetId) : ICommand;
