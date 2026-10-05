using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests.Fakes;

public sealed class InMemoryPrototypeRepository : IPrototypeRepository
{
    private readonly HashSet<PrototypeId> _prototypeIds = new();

    public void Add(PrototypeId prototypeId)
    {
        _prototypeIds.Add(prototypeId);
    }

    public Task<bool> ExistsAsync(
        PrototypeId prototypeId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(_prototypeIds.Contains(prototypeId));
    }
}
