using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.References;

namespace RapidCMS.Domain.Tests;

public class ComponentReferenceTests
{
    [Fact]
    public void Component_Can_Add_Reference()
    {
        var sourceNode = Node.Create("Button");
        var targetNode = Node.Create("Target");

        var component = Component.Create(
            sourceNode.Id,
            ComponentType.Create("Button"));

        var reference = NodeReference.Create(targetNode.Id);

        component.AddReference(reference);

        Assert.Single(component.References);
        Assert.Equal(targetNode.Id, component.References[0].TargetNodeId);
    }

    [Fact]
    public void Component_Does_Not_Add_Duplicate_Reference()
    {
        var sourceNode = Node.Create("Button");
        var targetNode = Node.Create("Target");

        var component = Component.Create(
            sourceNode.Id,
            ComponentType.Create("Button"));

        var reference = NodeReference.Create(targetNode.Id);

        component.AddReference(reference);
        component.AddReference(reference);

        Assert.Single(component.References);
    }

    [Fact]
    public void Component_Can_Remove_Reference()
    {
        var sourceNode = Node.Create("Button");
        var targetNode = Node.Create("Target");

        var component = Component.Create(
            sourceNode.Id,
            ComponentType.Create("Button"));

        var reference = NodeReference.Create(targetNode.Id);

        component.AddReference(reference);
        component.RemoveReference(reference);

        Assert.Empty(component.References);
    }
}
