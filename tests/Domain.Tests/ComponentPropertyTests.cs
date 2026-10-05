using RapidCMS.Domain.Components;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public class ComponentPropertyTests
{
    [Fact]
    public void Component_Can_Set_Property()
    {
        var node = Node.Create("Button");
        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Submit"));

        Assert.Single(component.Properties);
        Assert.Equal("text", component.Properties[0].Name);
        Assert.Equal("Submit", component.Properties[0].Value);
    }

    [Fact]
    public void Component_Can_Update_Existing_Property()
    {
        var node = Node.Create("Button");
        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Submit"));

        component.SetProperty(
            ComponentProperty.Create("text", "Save"));

        Assert.Single(component.Properties);
        Assert.Equal("Save", component.Properties[0].Value);
    }

    [Fact]
    public void Component_Can_Remove_Property()
    {
        var node = Node.Create("Button");
        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Submit"));

        component.RemoveProperty("text");

        Assert.Empty(component.Properties);
    }
}
