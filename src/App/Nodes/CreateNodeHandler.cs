using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Commands;
using RapidCMS.Contracts.Nodes;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Application.Nodes;

public sealed class CreateNodeHandler
    : ICommandHandler<CreateNodeCommand, NodeDto>
{
    private readonly IPageRepository _pageRepository;
    private readonly INodeRepository _nodeRepository;

    public CreateNodeHandler(
        IPageRepository pageRepository,
        INodeRepository nodeRepository)
    {
        _pageRepository = pageRepository;
        _nodeRepository = nodeRepository;
    }

    public async Task<CommandResult<NodeDto>> HandleAsync(
        CreateNodeCommand command,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (command.DocumentId == Guid.Empty)
            return CommandResult<NodeDto>.Failure(
                "Document ID cannot be empty.");

        if (command.PageId == Guid.Empty)
            return CommandResult<NodeDto>.Failure(
                "Page ID cannot be empty.");

        if (string.IsNullOrWhiteSpace(command.Name))
            return CommandResult<NodeDto>.Failure(
                "Node name cannot be empty.");

        var page = await _pageRepository.GetByIdAsync(
            new PageId(command.PageId),
            cancellationToken);

        if (page is null)
            return CommandResult<NodeDto>.Failure(
                $"Page '{command.PageId}' was not found.");

        if (page.DocumentId.Value != command.DocumentId)
            return CommandResult<NodeDto>.Failure(
                $"Page '{command.PageId}' does not belong to document '{command.DocumentId}'.");

        var node = Node.Create(command.Name);

        if (command.ParentId.HasValue)
        {
            var parent = await _nodeRepository.GetByIdAsync(
                new NodeId(command.ParentId.Value),
                cancellationToken);

            if (parent is null)
                return CommandResult<NodeDto>.Failure(
                    $"Parent node '{command.ParentId}' was not found.");

            parent.AddChild(node);

            await _nodeRepository.UpdateAsync(
                parent,
                cancellationToken);
        }
        else
        {
            if (page.RootNodeId.HasValue)
                return CommandResult<NodeDto>.Failure(
                    "Page already has a root node.");

            page.SetRootNode(node.Id);
        }

        await _nodeRepository.AddAsync(
            node,
            new PageId(command.PageId),
            cancellationToken);

        var dto = new NodeDto(
            node.Id.Value,
            node.Name,
            node.ParentId?.Value,
            node.Component?.Id.Value,
            node.Style?.Id.Value,
            node.Children
                .Select(x => x.Value)
                .ToList());

        return CommandResult<NodeDto>.Success(dto);
    }
}
