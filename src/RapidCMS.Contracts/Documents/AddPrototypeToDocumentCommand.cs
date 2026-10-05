using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record AddPrototypeToDocumentCommand(
    Guid DocumentId,
    Guid PrototypeId) : ICommand;
