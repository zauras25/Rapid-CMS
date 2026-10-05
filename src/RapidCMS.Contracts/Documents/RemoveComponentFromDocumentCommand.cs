using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record RemoveComponentFromDocumentCommand(
    Guid DocumentId,
    Guid ComponentId) : ICommand;
