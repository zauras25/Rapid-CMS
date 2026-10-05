using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests.Fakes;

public sealed class InMemoryVariableRepository : IVariableRepository
{
    private readonly HashSet<VariableId> _variableIds = new();

    public void Add(VariableId variableId)
    {
        _variableIds.Add(variableId);
    }

    public Task<bool> ExistsAsync(
        VariableId variableId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(_variableIds.Contains(variableId));
    }
}
