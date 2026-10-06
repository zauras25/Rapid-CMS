using RapidCMS.Application.Abstractions;
using RapidCMS.Contracts.Nodes;
using RapidCMS.Contracts.Queries;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Nodes;

public sealed class GetNodesHandler
    : IQueryHandler<GetNodeTreeQuery, IReadOnlyList<NodeDto>>
{
    private readonly IPageRepository _pageRepository;
    private readonly INodeRepository _nodeRepository;

    public GetNodesHandler(
        IPageRepository pageRepository,
        INodeRepository nodeRepository)
    {
        _pageRepository = pageRepository;
        _nodeRepository = nodeRepository;
    }

    public async Task<QueryResult<IReadOnlyList<NodeDto>>> HandleAsync(
        GetNodeTreeQuery query,
        CancellationToken cancellationToken = default)
    {
        var page = await _pageRepository.GetByIdAsync(
            new PageId(query.PageId),
            cancellationToken);

        if (page is null)
            return QueryResult<IReadOnlyList<NodeDto>>.Failure(
                $"Page '{query.PageId}' was not found.");

        if (page.DocumentId.Value != query.DocumentId)
            return QueryResult<IReadOnlyList<NodeDto>>.Failure(
                "Page does not belong to document.");

        var nodes = await _nodeRepository.GetByPageIdAsync(
            new PageId(query.PageId),
            cancellationToken);

        var result = nodes
            .Select(node => new NodeDto(
                node.Id.Value,
                node.Name,
                node.ParentId?.Value,
                node.Component?.Id.Value,
                node.Style?.Id.Value,
                node.Children
                    .Select(x => x.Value)
                    .ToList()))
            .ToList();

        return QueryResult<IReadOnlyList<NodeDto>>.Success(result);
    }
}
