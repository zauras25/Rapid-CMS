using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public class ComponentBehaviorTests
{
    [Fact]
    public void Component_Can_Add_Behavior()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        var behavior = ComponentBehavior.Create("OnClick");

        component.AddBehavior(behavior);

        Assert.Single(component.Behaviors);
        Assert.Contains(behavior, component.Behaviors);
    }

    [Fact]
    public void Component_Does_Not_Add_Duplicate_Behavior()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        var behavior = ComponentBehavior.Create("OnClick");

        component.AddBehavior(behavior);
        component.AddBehavior(behavior);

        Assert.Single(component.Behaviors);
    }

    [Fact]
    public void Component_Can_Remove_Behavior()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        var behavior = ComponentBehavior.Create("OnClick");

        component.AddBehavior(behavior);
        component.RemoveBehavior(behavior);

        Assert.Empty(component.Behaviors);
    }
}
