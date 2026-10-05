using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;
using RapidCMS.Domain.Prototypes;

namespace RapidCMS.Domain.Tests;

public class DocumentPrototypeTests
{
    [Fact]
    public void Document_Can_Add_And_Remove_Prototype()
    {
        var document = Document.Create();

        var source = Node.Create("Button");
        var target = Node.Create("Home");

        var prototype = PrototypeLink.Create(
            source.Id,
            target.Id,
            "Navigate");

        document.AddPrototype(prototype.Id);

        Assert.Contains(
            prototype.Id,
            document.PrototypeIds);

        document.RemovePrototype(prototype.Id);

        Assert.DoesNotContain(
            prototype.Id,
            document.PrototypeIds);
    }

    [Fact]
    public void Document_Cannot_Add_Duplicate_Prototype()
    {
        var document = Document.Create();

        var source = Node.Create("Button");
        var target = Node.Create("Home");

        var prototype = PrototypeLink.Create(
            source.Id,
            target.Id,
            "Navigate");

        document.AddPrototype(prototype.Id);

        Assert.Throws<InvalidOperationException>(
            () => document.AddPrototype(prototype.Id));
    }

    [Fact]
    public void Document_Cannot_Add_Empty_Prototype_Id()
    {
        var document = Document.Create();

        Assert.Throws<ArgumentException>(
            () => document.AddPrototype(
                new PrototypeId(Guid.Empty)));
    }
}
