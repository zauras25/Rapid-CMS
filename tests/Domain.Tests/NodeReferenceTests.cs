using RapidCMS.Domain.Identity;
using RapidCMS.Domain.References;

namespace RapidCMS.Domain.Tests;

public class NodeReferenceTests
{
    [Fact]
    public void NodeReference_Can_Be_Created()
    {
        var nodeId = NodeId.New();

        var reference = NodeReference.Create(nodeId);

        Assert.Equal(nodeId, reference.TargetNodeId);
    }

    [Fact]
    public void NodeReference_With_Same_Target_Is_Equal()
    {
        var nodeId = NodeId.New();

        var first = NodeReference.Create(nodeId);
        var second = NodeReference.Create(nodeId);

        Assert.Equal(first, second);
    }
}
