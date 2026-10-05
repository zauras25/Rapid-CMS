using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record DeleteDocumentCommand(
    Guid DocumentId) : ICommand;
