using RapidCMS.Application.Abstractions;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Tests.Fakes;

public sealed class InMemoryAssetRepository : IAssetRepository
{
    private readonly HashSet<AssetId> _assetIds = new();

    public void Add(AssetId assetId)
    {
        _assetIds.Add(assetId);
    }

    public Task<bool> ExistsAsync(
        AssetId assetId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(_assetIds.Contains(assetId));
    }
}
