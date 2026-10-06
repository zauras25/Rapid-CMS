using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Nodes;
using RapidCMS.Contracts.Queries;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Nodes;

public sealed class GetNodeByIdHandler
    : IQueryHandler<GetNodeQuery, NodeDto>
{
    private readonly IPageRepository _pageRepository;
    private readonly INodeRepository _nodeRepository;

    public GetNodeByIdHandler(
        IPageRepository pageRepository,
        INodeRepository nodeRepository)
    {
        _pageRepository = pageRepository;
        _nodeRepository = nodeRepository;
    }

    public async Task<QueryResult<NodeDto>> HandleAsync(
        GetNodeQuery query,
        CancellationToken cancellationToken = default)
    {
        var page = await _pageRepository.GetByIdAsync(
            new PageId(query.PageId),
            cancellationToken);

        if (page is null)
            return QueryResult<NodeDto>.Failure(
                $"Page '{query.PageId}' was not found.");

        if (page.DocumentId.Value != query.DocumentId)
            return QueryResult<NodeDto>.Failure(
                "Page does not belong to document.");

        var node = await _nodeRepository.GetByIdAsync(
            new NodeId(query.NodeId),
            cancellationToken);

        if (node is null)
            return QueryResult<NodeDto>.Failure(
                $"Node '{query.NodeId}' was not found.");

        var dto = new NodeDto(
            node.Id.Value,
            node.Name,
            node.ParentId?.Value,
            node.Component?.Id.Value,
            node.Style?.Id.Value,
            node.Children.Select(x => x.Value).ToList());

        return QueryResult<NodeDto>.Success(dto);
    }
}
