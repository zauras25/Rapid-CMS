using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Prototypes;

namespace RapidCMS.Domain.Tests;

public class PrototypeLinkTests
{
    [Fact]
    public void PrototypeLink_Can_Be_Created()
    {
        var source = Node.Create("Button");
        var target = Node.Create("Home");

        var link = PrototypeLink.Create(
            source.Id,
            target.Id,
            "Navigate");

        Assert.NotEqual(Guid.Empty, link.Id.Value);
        Assert.Equal(source.Id, link.SourceNodeId);
        Assert.Equal(target.Id, link.TargetNodeId);
        Assert.Equal("Navigate", link.Action);
    }

    [Fact]
    public void PrototypeLink_Cannot_Target_Its_Source()
    {
        var node = Node.Create("Button");

        Assert.Throws<InvalidOperationException>(
            () => PrototypeLink.Create(
                node.Id,
                node.Id,
                "Navigate"));
    }

    [Fact]
    public void PrototypeLink_Can_Change_Action()
    {
        var source = Node.Create("Button");
        var target = Node.Create("Home");

        var link = PrototypeLink.Create(
            source.Id,
            target.Id,
            "Navigate");

        link.ChangeAction("OpenOverlay");

        Assert.Equal("OpenOverlay", link.Action);
    }
}
