using RapidCMS.Domain.Components;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public class ComponentTests
{
    [Fact]
    public void Component_Can_Be_Created()
    {
        var node = Node.Create("Button");
        var type = ComponentType.Create("Button");

        var component = Component.Create(node.Id, type);

        Assert.Equal(node.Id, component.Id);
        Assert.Equal(type, component.Type);
    }

    [Fact]
    public void Component_Can_Change_Type()
    {
        var node = Node.Create("Element");

        var button = ComponentType.Create("Button");
        var text = ComponentType.Create("Text");

        var component = Component.Create(node.Id, button);

        component.ChangeType(text);

        Assert.Equal(text, component.Type);
    }
}
