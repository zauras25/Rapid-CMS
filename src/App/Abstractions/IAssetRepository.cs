using RapidCMS.Domain.Identity;

namespace RapidCMS.Application.Abstractions;

public interface IAssetRepository
{
    Task<bool> ExistsAsync(
        AssetId assetId,
        CancellationToken cancellationToken = default);
}
