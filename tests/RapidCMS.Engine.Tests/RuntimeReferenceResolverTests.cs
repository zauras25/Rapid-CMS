using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeReferenceResolverTests
{
    [Fact]
    public void Resolver_Finds_Root_Node()
    {
        var root = Node.Create("Root");

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var tree = RuntimeTree.From(root, nodes);
        var resolver = new RuntimeReferenceResolver(tree);

        var resolved = resolver.Resolve(root.Id);

        Assert.Equal(root.Id, resolved.Id);
    }

    [Fact]
    public void Resolver_Finds_Deep_Child_Node()
    {
        var root = Node.Create("Root");
        var level1 = Node.Create("Level1");
        var level2 = Node.Create("Level2");

        root.AddChild(level1);
        level1.AddChild(level2);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root,
            [level1.Id] = level1,
            [level2.Id] = level2
        };

        var tree = RuntimeTree.From(root, nodes);
        var resolver = new RuntimeReferenceResolver(tree);

        var resolved = resolver.Resolve(level2.Id);

        Assert.Equal(level2.Id, resolved.Id);
        Assert.Equal("Level2", resolved.Name);
    }

    [Fact]
    public void Resolver_Returns_False_For_Missing_Node()
    {
        var root = Node.Create("Root");

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var tree = RuntimeTree.From(root, nodes);
        var resolver = new RuntimeReferenceResolver(tree);

        var missingId = NodeId.New();

        var found = resolver.TryResolve(
            missingId,
            out var resolved);

        Assert.False(found);
        Assert.Null(resolved);
    }

    [Fact]
    public void Resolver_Throws_For_Missing_Node()
    {
        var root = Node.Create("Root");

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var tree = RuntimeTree.From(root, nodes);
        var resolver = new RuntimeReferenceResolver(tree);

        Assert.Throws<InvalidOperationException>(
            () => resolver.Resolve(NodeId.New()));
    }
}
