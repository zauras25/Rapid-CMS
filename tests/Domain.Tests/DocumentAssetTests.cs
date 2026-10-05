using RapidCMS.Domain.Assets;
using RapidCMS.Domain.Documents;

namespace RapidCMS.Domain.Tests;

public class DocumentAssetTests
{
    [Fact]
    public void Document_Can_Add_And_Remove_Asset()
    {
        var document = Document.Create();

        var asset = Asset.Create(
            "Logo",
            "/assets/logo.svg");

        document.AddAsset(asset.Id);

        Assert.Contains(asset.Id, document.AssetIds);

        document.RemoveAsset(asset.Id);

        Assert.DoesNotContain(asset.Id, document.AssetIds);
    }

    [Fact]
    public void Document_Cannot_Add_Duplicate_Asset()
    {
        var document = Document.Create();

        var asset = Asset.Create(
            "Logo",
            "/assets/logo.svg");

        document.AddAsset(asset.Id);

        Assert.Throws<InvalidOperationException>(
            () => document.AddAsset(asset.Id));
    }

    [Fact]
    public void Document_Cannot_Add_Empty_Asset_Id()
    {
        var document = Document.Create();

        Assert.Throws<ArgumentException>(
            () => document.AddAsset(
                new RapidCMS.Domain.Identity.AssetId(Guid.Empty)));
    }
}
