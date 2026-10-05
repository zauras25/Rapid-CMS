using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record RemovePageFromDocumentCommand(
    Guid DocumentId,
    Guid PageId) : ICommand;
