using RapidCMS.Domain.Components;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeTreeBuilderTests
{
    [Fact]
    public void RuntimeTreeBuilder_Copies_Child_Tree()
    {
        var root = Node.Create("Container");
        var button = Node.Create("Button");
        var text = Node.Create("Text");

        root.AddChild(button);
        button.AddChild(text);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root,
            [button.Id] = button,
            [text.Id] = text
        };

        var builder = new RuntimeTreeBuilder(nodes);

        var runtimeTree = builder.Build(root.Id);

        Assert.Equal(
            "Container",
            runtimeTree.Root.Name);

        Assert.Single(
            runtimeTree.Root.Children);

        var runtimeButton =
            runtimeTree.Root.Children[0];

        Assert.Equal(
            button.Id,
            runtimeButton.Id);

        Assert.Equal(
            "Button",
            runtimeButton.Name);

        Assert.Single(
            runtimeButton.Children);

        Assert.Equal(
            text.Id,
            runtimeButton.Children[0].Id);
    }

    [Fact]
    public void RuntimeTreeBuilder_Rejects_Missing_Root()
    {
        var root = Node.Create("Container");

        var nodes = new Dictionary<NodeId, Node>();

        var builder = new RuntimeTreeBuilder(nodes);

        Assert.Throws<InvalidOperationException>(
            () => builder.Build(root.Id));
    }

    [Fact]
    public void RuntimeTreeBuilder_Rejects_Missing_Child()
    {
        var root = Node.Create("Container");
        var child = Node.Create("Button");

        root.AddChild(child);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var builder = new RuntimeTreeBuilder(nodes);

        Assert.Throws<InvalidOperationException>(
            () => builder.Build(root.Id));
    }

    [Fact]
    public void RuntimeTreeBuilder_Copies_Component()
    {
        var root = Node.Create("Button");

        var component = Component.Create(
            root.Id,
            ComponentType.Create("Button"));

        component.SetProperty(
            ComponentProperty.Create("text", "Save"));

        root.AttachComponent(component);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var builder = new RuntimeTreeBuilder(nodes);

        var runtimeTree = builder.Build(root.Id);

        Assert.NotNull(
            runtimeTree.Root.Component);

        Assert.Equal(
            "Save",
            runtimeTree.Root.Component!.Properties["text"]);
    }
}
