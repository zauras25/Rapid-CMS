using RapidCMS.Domain.Styles;

namespace RapidCMS.Domain.Tests;

public class StylePropertyTests
{
    [Fact]
    public void StyleProperty_Can_Be_Created()
    {
        var property = StyleProperty.Create(
            "color",
            "#FF0000");

        Assert.Equal("color", property.Name);
        Assert.Equal("#FF0000", property.Value);
    }

    [Fact]
    public void StyleProperty_With_Same_Name_And_Value_Is_Equal()
    {
        var first = StyleProperty.Create(
            "color",
            "#FF0000");

        var second = StyleProperty.Create(
            "color",
            "#FF0000");

        Assert.Equal(first, second);
    }
}
