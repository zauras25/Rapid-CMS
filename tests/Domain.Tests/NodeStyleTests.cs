using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Styles;

namespace RapidCMS.Domain.Tests;

public class NodeStyleTests
{
    [Fact]
    public void Node_Can_Attach_Its_Style()
    {
        var node = Node.Create("Button");

        var style = Style.Create(node.Id);

        node.AttachStyle(style);

        Assert.NotNull(node.Style);
        Assert.Equal(node.Id, node.Style!.Id);
    }

    [Fact]
    public void Node_Cannot_Attach_Style_From_Another_Node()
    {
        var node = Node.Create("Button");
        var anotherNode = Node.Create("Text");

        var style = Style.Create(anotherNode.Id);

        Assert.Throws<InvalidOperationException>(
            () => node.AttachStyle(style));
    }

    [Fact]
    public void Node_Cannot_Attach_Two_Styles()
    {
        var node = Node.Create("Button");

        var first = Style.Create(node.Id);
        var second = Style.Create(node.Id);

        node.AttachStyle(first);

        Assert.Throws<InvalidOperationException>(
            () => node.AttachStyle(second));
    }
}
