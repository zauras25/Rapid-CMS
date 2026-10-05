using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record AddVariableToDocumentCommand(
    Guid DocumentId,
    Guid VariableId) : ICommand;
