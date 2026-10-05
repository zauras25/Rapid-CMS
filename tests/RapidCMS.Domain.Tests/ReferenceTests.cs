using RapidCMS.Domain.References;

namespace RapidCMS.Domain.Tests;

public class ReferenceTests
{
    [Fact]
    public void DocumentReference_Can_Be_Created()
    {
        var sourceId = Guid.NewGuid();
        var targetId = Guid.NewGuid();

        var reference = DocumentReference.Create(
            "Node",
            sourceId,
            "Component",
            targetId,
            "uses");

        Assert.NotEqual(Guid.Empty, reference.Id.Value);
        Assert.Equal("Node", reference.SourceType);
        Assert.Equal(sourceId, reference.SourceId);
        Assert.Equal("Component", reference.TargetType);
        Assert.Equal(targetId, reference.TargetId);
        Assert.Equal("uses", reference.Relation);
    }

    [Fact]
    public void DocumentReference_Cannot_Target_Itself()
    {
        var id = Guid.NewGuid();

        Assert.Throws<InvalidOperationException>(
            () => DocumentReference.Create(
                "Node",
                id,
                "Node",
                id,
                "reference"));
    }

    [Fact]
    public void DocumentReference_Cannot_Have_Empty_Source_Id()
    {
        Assert.Throws<ArgumentException>(
            () => DocumentReference.Create(
                "Node",
                Guid.Empty,
                "Node",
                Guid.NewGuid(),
                "reference"));
    }

    [Fact]
    public void DocumentReference_Cannot_Have_Empty_Target_Id()
    {
        Assert.Throws<ArgumentException>(
            () => DocumentReference.Create(
                "Node",
                Guid.NewGuid(),
                "Node",
                Guid.Empty,
                "reference"));
    }

    [Fact]
    public void DocumentReference_Cannot_Have_Empty_Relation()
    {
        Assert.Throws<ArgumentException>(
            () => DocumentReference.Create(
                "Node",
                Guid.NewGuid(),
                "Node",
                Guid.NewGuid(),
                ""));
    }

    [Fact]
    public void DocumentReference_Can_Change_Relation()
    {
        var reference = DocumentReference.Create(
            "Node",
            Guid.NewGuid(),
            "Component",
            Guid.NewGuid(),
            "uses");

        reference.ChangeRelation("contains");

        Assert.Equal("contains", reference.Relation);
    }

    [Fact]
    public void NodeReference_Can_Be_Created()
    {
        var nodeId = RapidCMS.Domain.Identity.NodeId.New();

        var reference = NodeReference.Create(nodeId);

        Assert.Equal(nodeId, reference.TargetNodeId);
    }

    [Fact]
    public void NodeReference_Has_Value_Object_Equality()
    {
        var nodeId = RapidCMS.Domain.Identity.NodeId.New();

        var first = NodeReference.Create(nodeId);
        var second = NodeReference.Create(nodeId);

        Assert.Equal(first, second);
    }

    [Fact]
    public void NodeReference_Cannot_Have_Empty_Target()
    {
        Assert.Throws<ArgumentException>(
            () => NodeReference.Create(
                new RapidCMS.Domain.Identity.NodeId(Guid.Empty)));
    }
}
