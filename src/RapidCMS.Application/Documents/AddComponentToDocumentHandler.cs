using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class AddComponentToDocumentHandler
    : ICommandHandler<AddComponentToDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IComponentRepository _componentRepository;

    public AddComponentToDocumentHandler(
        IDocumentRepository documentRepository,
        IComponentRepository componentRepository)
    {
        _documentRepository = documentRepository;
        _componentRepository = componentRepository;
    }

    public async Task<CommandResult> HandleAsync(
        AddComponentToDocumentCommand command,
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

        var componentExists = await _componentRepository.ExistsAsync(
            new ComponentId(command.ComponentId),
            cancellationToken);

        if (!componentExists)
            return CommandResult.Failure(
                $"Component '{command.ComponentId}' was not found.");

        try
        {
            document.AddComponent(
                new ComponentId(command.ComponentId));
        }
        catch (InvalidOperationException exception)
        {
            return CommandResult.Failure(exception.Message);
        }

        return CommandResult.Success();
    }
}
