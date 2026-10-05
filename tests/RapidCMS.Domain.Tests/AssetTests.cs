using RapidCMS.Domain.Assets;

namespace RapidCMS.Domain.Tests;

public class AssetTests
{
    [Fact]
    public void Asset_Can_Be_Created()
    {
        var asset = Asset.Create(
            "Logo",
            "/assets/logo.svg");

        Assert.NotEqual(Guid.Empty, asset.Id.Value);
        Assert.Equal("Logo", asset.Name);
        Assert.Equal("/assets/logo.svg", asset.Source);
    }

    [Fact]
    public void Asset_Can_Be_Renamed()
    {
        var asset = Asset.Create(
            "Logo",
            "/assets/logo.svg");

        asset.Rename("Main Logo");

        Assert.Equal("Main Logo", asset.Name);
    }

    [Fact]
    public void Asset_Can_Change_Source()
    {
        var asset = Asset.Create(
            "Logo",
            "/assets/logo.svg");

        asset.ChangeSource("/assets/main-logo.svg");

        Assert.Equal(
            "/assets/main-logo.svg",
            asset.Source);
    }
}
