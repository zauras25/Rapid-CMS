using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record AddAssetToDocumentCommand(
    Guid DocumentId,
    Guid AssetId) : ICommand;
