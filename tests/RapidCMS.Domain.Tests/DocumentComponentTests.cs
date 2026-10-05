using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Tests;

public sealed class DocumentComponentTests
{
    [Fact]
    public void AddComponent_Adds_Component_To_Document()
    {
        var document = Document.Create();
        var componentId = ComponentId.New();

        document.AddComponent(componentId);

        Assert.Contains(componentId, document.ComponentIds);
    }

    [Fact]
    public void AddComponent_With_Empty_Id_Throws()
    {
        var document = Document.Create();

        Assert.Throws<ArgumentException>(
            () => document.AddComponent(new ComponentId(Guid.Empty)));
    }

    [Fact]
    public void AddComponent_When_Already_Attached_Throws()
    {
        var document = Document.Create();
        var componentId = ComponentId.New();

        document.AddComponent(componentId);

        Assert.Throws<InvalidOperationException>(
            () => document.AddComponent(componentId));
    }

    [Fact]
    public void RemoveComponent_Removes_Component_From_Document()
    {
        var document = Document.Create();
        var componentId = ComponentId.New();

        document.AddComponent(componentId);

        document.RemoveComponent(componentId);

        Assert.DoesNotContain(componentId, document.ComponentIds);
    }

    [Fact]
    public void RemoveComponent_When_Not_Attached_Is_Idempotent()
    {
        var document = Document.Create();
        var componentId = ComponentId.New();

        document.RemoveComponent(componentId);

        Assert.DoesNotContain(componentId, document.ComponentIds);
    }
}
