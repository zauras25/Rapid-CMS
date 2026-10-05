using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record RemoveVariableFromDocumentCommand(
    Guid DocumentId,
    Guid VariableId) : ICommand;
