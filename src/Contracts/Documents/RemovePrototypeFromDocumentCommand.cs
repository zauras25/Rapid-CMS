using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record RemovePrototypeFromDocumentCommand(
    Guid DocumentId,
    Guid PrototypeId) : ICommand;
