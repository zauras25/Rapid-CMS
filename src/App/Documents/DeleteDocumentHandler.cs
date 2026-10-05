using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class DeleteDocumentHandler
    : ICommandHandler<DeleteDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;

    public DeleteDocumentHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<CommandResult> HandleAsync(
        DeleteDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
        {
            return CommandResult.Failure(
                "Document ID cannot be empty.");
        }

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(command.DocumentId),
            cancellationToken);

        if (document is null)
        {
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");
        }

        var deleted = await _documentRepository.DeleteAsync(
            document.Id,
            cancellationToken);

        if (!deleted)
        {
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' could not be deleted.");
        }

        return CommandResult.Success();
    }
}
