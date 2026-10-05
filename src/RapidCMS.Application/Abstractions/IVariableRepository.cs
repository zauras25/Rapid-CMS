using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Abstractions;

public interface IVariableRepository
{
    Task<bool> ExistsAsync(
        VariableId variableId,
        CancellationToken cancellationToken = default);
}
