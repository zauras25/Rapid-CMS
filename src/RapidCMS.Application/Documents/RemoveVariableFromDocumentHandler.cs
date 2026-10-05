using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class RemoveVariableFromDocumentHandler
    : ICommandHandler<RemoveVariableFromDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;

    public RemoveVariableFromDocumentHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<CommandResult> HandleAsync(
        RemoveVariableFromDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult.Failure("Document ID cannot be empty.");

        if (command.VariableId == Guid.Empty)
            return CommandResult.Failure("Variable ID cannot be empty.");

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(command.DocumentId),
            cancellationToken);

        if (document is null)
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");

        document.RemoveVariable(new VariableId(command.VariableId));

        return CommandResult.Success();
    }
}
