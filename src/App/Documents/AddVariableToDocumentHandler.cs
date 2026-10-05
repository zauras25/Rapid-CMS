using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class AddVariableToDocumentHandler
    : ICommandHandler<AddVariableToDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IVariableRepository _variableRepository;

    public AddVariableToDocumentHandler(
        IDocumentRepository documentRepository,
        IVariableRepository variableRepository)
    {
        _documentRepository = documentRepository;
        _variableRepository = variableRepository;
    }

    public async Task<CommandResult> HandleAsync(
        AddVariableToDocumentCommand command,
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

        var variableExists = await _variableRepository.ExistsAsync(
            new VariableId(command.VariableId),
            cancellationToken);

        if (!variableExists)
            return CommandResult.Failure(
                $"Variable '{command.VariableId}' was not found.");

        try
        {
            document.AddVariable(new VariableId(command.VariableId));
        }
        catch (InvalidOperationException exception)
        {
            return CommandResult.Failure(exception.Message);
        }

        return CommandResult.Success();
    }
}

