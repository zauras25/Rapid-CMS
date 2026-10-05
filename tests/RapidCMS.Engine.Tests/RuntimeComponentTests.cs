using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.References;
using RapidCMS.Domain.Styles;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeComponentTests
{
    [Fact]
    public void RuntimeComponent_Copies_Properties()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Save"));

        component.SetProperty(
            ComponentProperty.Create("disabled", "true"));

        var runtime = RuntimeComponent.From(component);

        Assert.Equal("Button", runtime.Type);
        Assert.Equal("Save", runtime.Properties["text"]);
        Assert.Equal("true", runtime.Properties["disabled"]);
    }

    [Fact]
    public void RuntimeComponent_Copies_Behaviors()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        var behavior = ComponentBehavior.Create("OnClick");

        component.AddBehavior(behavior);

        var runtime = RuntimeComponent.From(component);

        Assert.Single(runtime.Behaviors);
        Assert.Contains(behavior, runtime.Behaviors);
    }

    [Fact]
    public void RuntimeComponent_Copies_References()
    {
        var node = Node.Create("Button");
        var target = Node.Create("Target");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        var reference = NodeReference.Create(target.Id);

        component.AddReference(reference);

        var runtime = RuntimeComponent.From(component);

        Assert.Single(runtime.References);
        Assert.Equal(
            target.Id,
            runtime.References[0].TargetNodeId);
    }
}
