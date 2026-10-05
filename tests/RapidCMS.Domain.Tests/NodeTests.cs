using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public class NodeTests
{
    [Fact]
    public void Node_Can_Be_Created()
    {
        var node = Node.Create("Root");

        Assert.NotEqual(Guid.Empty, node.Id.Value);
        Assert.Equal("Root", node.Name);
        Assert.Null(node.ParentId);
        Assert.Empty(node.Children);
    }

    [Fact]
    public void Node_Can_Be_Renamed()
    {
        var node = Node.Create("Root");

        node.Rename("Main");

        Assert.Equal("Main", node.Name);
    }

    [Fact]
    public void Node_Cannot_Be_Created_With_Empty_Name()
    {
        Assert.Throws<ArgumentException>(
            () => Node.Create(""));
    }

    [Fact]
    public void Node_Can_Add_Child()
    {
        var parent = Node.Create("Parent");
        var child = Node.Create("Child");

        parent.AddChild(child);

        Assert.Contains(child.Id, parent.Children);
        Assert.Equal(parent.Id, child.ParentId);
    }

    [Fact]
    public void Node_Cannot_Add_Same_Child_Twice()
    {
        var parent = Node.Create("Parent");
        var child = Node.Create("Child");

        parent.AddChild(child);

        Assert.Throws<InvalidOperationException>(
            () => parent.AddChild(child));
    }

    [Fact]
    public void Node_Cannot_Have_Two_Parents()
    {
        var first = Node.Create("First");
        var second = Node.Create("Second");
        var child = Node.Create("Child");

        first.AddChild(child);

        Assert.Throws<InvalidOperationException>(
            () => second.AddChild(child));
    }

    [Fact]
    public void Node_Can_Remove_Child()
    {
        var parent = Node.Create("Parent");
        var child = Node.Create("Child");

        parent.AddChild(child);
        parent.RemoveChild(child);

        Assert.DoesNotContain(child.Id, parent.Children);
        Assert.Null(child.ParentId);
    }
}
