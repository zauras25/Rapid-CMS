using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Styles;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeStyleTests
{
    [Fact]
    public void RuntimeStyle_Copies_Properties()
    {
        var node = Node.Create("Button");

        var style = Style.Create(node.Id);

        style.SetProperty(
            StyleProperty.Create("color", "#FF0000"));

        style.SetProperty(
            StyleProperty.Create("padding", "12px"));

        var runtime = RuntimeStyle.From(style);

        Assert.Equal("#FF0000", runtime.Properties["color"]);
        Assert.Equal("12px", runtime.Properties["padding"]);
    }

    [Fact]
    public void RuntimeStyle_Without_Properties_Is_Empty()
    {
        var node = Node.Create("Button");

        var style = Style.Create(node.Id);

        var runtime = RuntimeStyle.From(style);

        Assert.Empty(runtime.Properties);
    }
}
