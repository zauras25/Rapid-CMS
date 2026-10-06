using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Nodes;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Nodes;

public sealed class UpdateNodeHandler
    : ICommandHandler<UpdateNodeCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly INodeRepository _nodeRepository;

    public UpdateNodeHandler(
        IPageRepository pageRepository,
        INodeRepository nodeRepository)
    {
        _pageRepository = pageRepository;
        _nodeRepository = nodeRepository;
    }

    public async Task<CommandResult> HandleAsync(
        UpdateNodeCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult.Failure(
                "Document ID cannot be empty.");

        if (command.PageId == Guid.Empty)
            return CommandResult.Failure(
                "Page ID cannot be empty.");

        if (command.NodeId == Guid.Empty)
            return CommandResult.Failure(
                "Node ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(command.Name))
            return CommandResult.Failure(
                "Node name cannot be empty.");

        var page = await _pageRepository.GetByIdAsync(
            new PageId(command.PageId),
            cancellationToken);

        if (page is null)
            return CommandResult.Failure(
                $"Page '{command.PageId}' was not found.");

        if (page.DocumentId.Value != command.DocumentId)
            return CommandResult.Failure(
                $"Page '{command.PageId}' does not belong to document '{command.DocumentId}'.");

        var node = await _nodeRepository.GetByIdAsync(
            new NodeId(command.NodeId),
            cancellationToken);

        if (node is null)
            return CommandResult.Failure(
                $"Node '{command.NodeId}' was not found.");

        node.Rename(command.Name);

        await _nodeRepository.UpdateAsync(
            node,
            cancellationToken);

        return CommandResult.Success();
    }
}
