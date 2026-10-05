using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class RemoveComponentFromDocumentHandler
    : ICommandHandler<RemoveComponentFromDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;

    public RemoveComponentFromDocumentHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<CommandResult> HandleAsync(
        RemoveComponentFromDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult.Failure(
                "Document ID cannot be empty.");

        if (command.ComponentId == Guid.Empty)
            return CommandResult.Failure(
                "Component ID cannot be empty.");

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(command.DocumentId),
            cancellationToken);

        if (document is null)
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");

        document.RemoveComponent(
            new ComponentId(command.ComponentId));

        return CommandResult.Success();
    }
}
