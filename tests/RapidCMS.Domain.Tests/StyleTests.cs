using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Styles;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public class StyleTests
{
    [Fact]
    public void Style_Can_Be_Created_For_Node()
    {
        var node = Node.Create("Button");

        var style = Style.Create(node.Id);

        Assert.Equal(node.Id, style.Id);
        Assert.Empty(style.Properties);
    }

    [Fact]
    public void Style_Can_Set_Property()
    {
        var node = Node.Create("Button");

        var style = Style.Create(node.Id);

        style.SetProperty(
            StyleProperty.Create("color", "#FF0000"));

        Assert.Single(style.Properties);
        Assert.Equal("color", style.Properties[0].Name);
        Assert.Equal("#FF0000", style.Properties[0].Value);
    }

    [Fact]
    public void Style_Can_Update_Existing_Property()
    {
        var node = Node.Create("Button");

        var style = Style.Create(node.Id);

        style.SetProperty(
            StyleProperty.Create("color", "#FF0000"));

        style.SetProperty(
            StyleProperty.Create("color", "#00FF00"));

        Assert.Single(style.Properties);
        Assert.Equal("#00FF00", style.Properties[0].Value);
    }
}
