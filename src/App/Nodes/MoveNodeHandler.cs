using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Nodes;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Application.Nodes;

public sealed class MoveNodeHandler
    : ICommandHandler<MoveNodeCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly INodeRepository _nodeRepository;

    public MoveNodeHandler(
        IPageRepository pageRepository,
        INodeRepository nodeRepository)
    {
        _pageRepository = pageRepository;
        _nodeRepository = nodeRepository;
    }

    public async Task<CommandResult> HandleAsync(
        MoveNodeCommand command,
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

        if (command.Index is < 0)
            return CommandResult.Failure(
                "Index cannot be negative.");

        if (command.NewParentId == command.NodeId)
            return CommandResult.Failure(
                "A node cannot be its own parent.");

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

        Node? newParent = null;

        if (command.NewParentId.HasValue)
        {
            if (command.NewParentId.Value == Guid.Empty)
                return CommandResult.Failure(
                    "New parent ID cannot be empty.");

            newParent = await _nodeRepository.GetByIdAsync(
                new NodeId(command.NewParentId.Value),
                cancellationToken);

            if (newParent is null)
                return CommandResult.Failure(
                    $"Parent node '{command.NewParentId}' was not found.");

            // Check whether new parent is inside the node's subtree.
            var current = newParent;

            while (current.ParentId.HasValue)
            {
                if (current.ParentId.Value == node.Id)
                    return CommandResult.Failure(
                        "Moving this node would create a cycle.");

                var parent = await _nodeRepository.GetByIdAsync(
                    current.ParentId.Value,
                    cancellationToken);

                if (parent is null)
                    break;

                current = parent;
            }
        }

        var oldParentId = node.ParentId;

        // Remove from old parent.
        if (oldParentId.HasValue)
        {
            var oldParent = await _nodeRepository.GetByIdAsync(
                oldParentId.Value,
                cancellationToken);

            if (oldParent is not null)
            {
                oldParent.RemoveChild(node);

                await _nodeRepository.UpdateAsync(
                    oldParent,
                    cancellationToken);
            }
        }

        // Move to root.
        if (newParent is null)
        {
            if (page.RootNodeId.HasValue &&
                page.RootNodeId.Value != node.Id)
            {
                return CommandResult.Failure(
                    "Page already has another root node.");
            }

            node.MoveTo(null);
        }
        else
        {
            newParent.AddChild(node);

            await _nodeRepository.UpdateAsync(
                newParent,
                cancellationToken);
        }

        await _nodeRepository.UpdateAsync(
            node,
            cancellationToken);

        return CommandResult.Success();
    }
}
