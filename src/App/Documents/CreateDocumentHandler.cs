using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class CreateDocumentHandler
    : ICommandHandler<CreateDocumentCommand, Document>
{
    private readonly IDocumentRepository _repository;

    public CreateDocumentHandler(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult<Document>> HandleAsync(
        CreateDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
        {
            return CommandResult<Document>.Failure(
                "Document ID cannot be empty.");
        }

        var requestedId = new DocumentId(command.DocumentId);

        var existingDocument = await _repository.GetByIdAsync(
            requestedId,
            cancellationToken);

        if (existingDocument is not null)
        {
            return CommandResult<Document>.Failure(
                $"Document '{command.DocumentId}' already exists.");
        }

        var document = Document.Create(requestedId);

        await _repository.AddAsync(
            document,
            cancellationToken);

        return CommandResult<Document>.Success(document);
    }
}

