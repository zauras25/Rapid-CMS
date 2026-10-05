using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public class NodeComponentTests
{
    [Fact]
    public void Node_Can_Attach_Its_Component()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        node.AttachComponent(component);

        Assert.NotNull(node.Component);
        Assert.Equal(component.Id, node.Component!.Id);
        Assert.Equal("Button", node.Component.Type.Value);
    }

    [Fact]
    public void Node_Cannot_Attach_Component_From_Another_Node()
    {
        var node = Node.Create("Button");
        var anotherNode = Node.Create("Text");

        var component = Component.Create(
            anotherNode.Id,
            ComponentType.Create("Text"));

        Assert.Throws<InvalidOperationException>(
            () => node.AttachComponent(component));
    }

    [Fact]
    public void Node_Cannot_Attach_Two_Components()
    {
        var node = Node.Create("Button");

        var first = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        var second = Component.Create(
            node.Id,
            ComponentType.Create("Text"));

        node.AttachComponent(first);

        Assert.Throws<InvalidOperationException>(
            () => node.AttachComponent(second));
    }
}
