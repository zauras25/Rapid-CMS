using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record CreateDocumentCommand(
    Guid DocumentId) : ICommand;
