using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class RemovePrototypeFromDocumentHandler
    : ICommandHandler<RemovePrototypeFromDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;

    public RemovePrototypeFromDocumentHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<CommandResult> HandleAsync(
        RemovePrototypeFromDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult.Failure("Document ID cannot be empty.");

        if (command.PrototypeId == Guid.Empty)
            return CommandResult.Failure("Prototype ID cannot be empty.");

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(command.DocumentId),
            cancellationToken);

        if (document is null)
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");

        document.RemovePrototype(
            new PrototypeId(command.PrototypeId));

        return CommandResult.Success();
    }
}
