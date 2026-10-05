using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class AddPrototypeToDocumentHandler
    : ICommandHandler<AddPrototypeToDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IPrototypeRepository _prototypeRepository;

    public AddPrototypeToDocumentHandler(
        IDocumentRepository documentRepository,
        IPrototypeRepository prototypeRepository)
    {
        _documentRepository = documentRepository;
        _prototypeRepository = prototypeRepository;
    }

    public async Task<CommandResult> HandleAsync(
        AddPrototypeToDocumentCommand command,
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

        var prototypeExists = await _prototypeRepository.ExistsAsync(
            new PrototypeId(command.PrototypeId),
            cancellationToken);

        if (!prototypeExists)
            return CommandResult.Failure(
                $"Prototype '{command.PrototypeId}' was not found.");

        try
        {
            document.AddPrototype(
                new PrototypeId(command.PrototypeId));
        }
        catch (InvalidOperationException exception)
        {
            return CommandResult.Failure(exception.Message);
        }

        return CommandResult.Success();
    }
}
