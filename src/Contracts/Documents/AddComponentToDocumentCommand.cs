using RapidCMS.Contracts.Commands;

namespace RapidCMS.Contracts.Documents;

public sealed record AddComponentToDocumentCommand(
    Guid DocumentId,
    Guid ComponentId) : ICommand;
