using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests.Fakes;

public sealed class InMemoryComponentRepository : IComponentRepository
{
    private readonly HashSet<ComponentId> _componentIds = new();

    public void Add(ComponentId componentId)
    {
        _componentIds.Add(componentId);
    }

    public Task<bool> ExistsAsync(
        ComponentId componentId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(_componentIds.Contains(componentId));
    }
}
