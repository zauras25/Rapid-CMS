using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Abstractions;

public interface IComponentRepository
{
    Task<bool> ExistsAsync(
        ComponentId componentId,
        CancellationToken cancellationToken = default);
}
