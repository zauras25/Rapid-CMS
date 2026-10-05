using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Documents;

public sealed class AddPageToDocumentHandler
    : ICommandHandler<AddPageToDocumentCommand>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IPageRepository _pageRepository;

    public AddPageToDocumentHandler(
        IDocumentRepository documentRepository,
        IPageRepository pageRepository)
    {
        _documentRepository = documentRepository;
        _pageRepository = pageRepository;
    }

    public async Task<CommandResult> HandleAsync(
        AddPageToDocumentCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
        {
            return CommandResult.Failure(
                "Document ID cannot be empty.");
        }

        if (command.PageId == Guid.Empty)
        {
            return CommandResult.Failure(
                "Page ID cannot be empty.");
        }

        var documentId = new DocumentId(command.DocumentId);
        var pageId = new PageId(command.PageId);

        var document = await _documentRepository.GetByIdAsync(
            documentId,
            cancellationToken);

        if (document is null)
        {
            return CommandResult.Failure(
                $"Document '{command.DocumentId}' was not found.");
        }

        var page = await _pageRepository.GetByIdAsync(
            pageId,
            cancellationToken);

        if (page is null)
        {
            return CommandResult.Failure(
                $"Page '{command.PageId}' was not found.");
        }

        try
        {
            document.AddPage(page.Id);
        }
        catch (InvalidOperationException exception)
        {
            return CommandResult.Failure(exception.Message);
        }

        return CommandResult.Success();
    }
}

