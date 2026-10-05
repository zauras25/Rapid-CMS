using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record AddPageToDocumentCommand(
    Guid DocumentId,
    Guid PageId) : ICommand;
