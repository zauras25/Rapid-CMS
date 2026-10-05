using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeNodeTests
{
    [Fact]
    public void RuntimeNode_Can_Be_Created_From_Domain_Node()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Save"));

        node.AttachComponent(component);

        var runtimeNode = RuntimeNode.From(node);

        Assert.Equal(node.Id, runtimeNode.Id);
        Assert.Equal("Button", runtimeNode.Name);
        Assert.NotNull(runtimeNode.Component);
        Assert.Equal("Button", runtimeNode.Component!.Type);
        Assert.Equal("Save", runtimeNode.Component.Properties["text"]);
    }

    [Fact]
    public void RuntimeNode_Without_Component_Has_No_Component()
    {
        var node = Node.Create("Container");

        var runtimeNode = RuntimeNode.From(node);

        Assert.Null(runtimeNode.Component);
    }
}
