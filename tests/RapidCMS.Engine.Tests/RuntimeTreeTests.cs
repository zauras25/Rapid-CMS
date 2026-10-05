using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeTreeTests
{
    [Fact]
    public void RuntimeTree_Can_Build_Single_Node()
    {
        var root = Node.Create("Root");

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        var tree = RuntimeTree.From(root, nodes);

        Assert.Equal(root.Id, tree.Root.Id);
        Assert.Equal("Root", tree.Root.Name);
        Assert.Empty(tree.Root.Children);
    }

    [Fact]
    public void RuntimeTree_Rejects_Missing_Child_Node()
    {
        var root = Node.Create("Root");
        var child = Node.Create("Child");

        root.AddChild(child);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root
        };

        Assert.Throws<InvalidOperationException>(
            () => RuntimeTree.From(root, nodes));
    }

    [Fact]
    public void RuntimeTree_Preserves_Deep_Hierarchy()
    {
        var root = Node.Create("Root");
        var level1 = Node.Create("Level1");
        var level2 = Node.Create("Level2");
        var level3 = Node.Create("Level3");

        root.AddChild(level1);
        level1.AddChild(level2);
        level2.AddChild(level3);

        var nodes = new Dictionary<NodeId, Node>
        {
            [root.Id] = root,
            [level1.Id] = level1,
            [level2.Id] = level2,
            [level3.Id] = level3
        };

        var tree = RuntimeTree.From(root, nodes);

        Assert.Equal(
            level1.Id,
            tree.Root.Children[0].Id);

        Assert.Equal(
            level2.Id,
            tree.Root.Children[0].Children[0].Id);

        Assert.Equal(
            level3.Id,
            tree.Root.Children[0].Children[0].Children[0].Id);
    }
}
