using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class RemovePageFromDocumentHandler
    : ICommandHandler<RemovePageFromDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;

    public RemovePageFromDocumentHandler(
        IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<CommandResult> HandleAsync(
        RemovePageFromDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult.Failure(
                "Document ID cannot be empty.");

        if (command.PageId == Guid.Empty)
            return CommandResult.Failure(
                "Page ID cannot be empty.");

        var document = await _documentRepository.GetByIdAsync(
            new DocumentId(command.DocumentId),
            cancellationToken);

        if (document is null)
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");

        document.RemovePage(new PageId(command.PageId));

        return CommandResult.Success();
    }
}
