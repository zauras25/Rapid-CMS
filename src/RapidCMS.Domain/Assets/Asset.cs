using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Assets;

public sealed class Asset : Entity<AssetId>
{
    public string Name { get; private set; }

    public string Source { get; private set; }

    private Asset(
        AssetId id,
        string name,
        string source)
        : base(id)
    {
        Name = name;
        Source = source;
    }

    public static Asset Create(
        string name,
        string source)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Asset name cannot be empty.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException(
                "Asset source cannot be empty.",
                nameof(source));

        return new Asset(
            AssetId.New(),
            name.Trim(),
            source.Trim());
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Asset name cannot be empty.",
                nameof(name));

        Name = name.Trim();
    }

    public void ChangeSource(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException(
                "Asset source cannot be empty.",
                nameof(source));

        Source = source.Trim();
    }
}
