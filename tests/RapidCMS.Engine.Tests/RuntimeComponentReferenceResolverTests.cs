using RapidCMS.Domain.Components;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.References;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeComponentReferenceResolverTests
{
    [Fact]
    public void Resolver_Resolves_Component_References()
    {
        var root = Node.Create("Root");
        var button = Node.Create("Button");
        var text = Node.Create("Text");

        root.AddChild(button);
        root.AddChild(text);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root,
            [button.Id] = button,
            [text.Id] = text
        };

        var component = Component.Create(
            root.Id,
            ComponentType.Create("Container"));

        component.AddReference(
            NodeReference.Create(button.Id));

        component.AddReference(
            NodeReference.Create(text.Id));

        var runtimeTree = RuntimeTree.From(root, nodes);
        var runtimeComponent = RuntimeComponent.From(component);

        var resolver =
            new RuntimeComponentReferenceResolver(runtimeTree);

        var resolved = resolver.Resolve(runtimeComponent);

        Assert.Equal(2, resolved.Count);
        Assert.Equal(button.Id, resolved[0].Id);
        Assert.Equal(text.Id, resolved[1].Id);
    }

    [Fact]
    public void Resolver_Returns_Empty_List_When_Component_Has_No_References()
    {
        var root = Node.Create("Root");

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var component = Component.Create(
            root.Id,
            ComponentType.Create("Container"));

        var runtimeTree = RuntimeTree.From(root, nodes);
        var runtimeComponent = RuntimeComponent.From(component);

        var resolver =
            new RuntimeComponentReferenceResolver(runtimeTree);

        var resolved = resolver.Resolve(runtimeComponent);

        Assert.Empty(resolved);
    }

    [Fact]
    public void Resolver_Rejects_Missing_Referenced_Node()
    {
        var root = Node.Create("Root");
        var target = Node.Create("Target");

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var component = Component.Create(
            root.Id,
            ComponentType.Create("Container"));

        component.AddReference(
            NodeReference.Create(target.Id));

        var runtimeTree = RuntimeTree.From(root, nodes);
        var runtimeComponent = RuntimeComponent.From(component);

        var resolver =
            new RuntimeComponentReferenceResolver(runtimeTree);

        Assert.Throws<InvalidOperationException>(
            () => resolver.Resolve(runtimeComponent));
    }
}
