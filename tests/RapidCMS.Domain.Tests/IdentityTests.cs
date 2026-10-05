using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Tests;

public class IdentityTests
{
    [Fact]
    public void ProjectId_New_Is_Not_Empty()
    {
        var id = ProjectId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void ProjectId_With_Same_Value_Is_Equal()
    {
        var value = Guid.NewGuid();

        var first = new ProjectId(value);
        var second = new ProjectId(value);

        Assert.Equal(first, second);
    }

    [Fact]
    public void Different_Identity_Types_Are_Not_Interchangeable()
    {
        var value = Guid.NewGuid();

        var projectId = new ProjectId(value);
        var documentId = new DocumentId(value);

        Assert.NotEqual(
            projectId.Value,
            Guid.Empty);

        Assert.NotEqual(
            documentId.Value,
            Guid.Empty);
    }

    [Fact]
    public void NodeId_With_Empty_Value_Is_Rejected()
    {
        Assert.Throws<ArgumentException>(
            () => new NodeId(Guid.Empty));
    }

    [Fact]
    public void DocumentId_With_Empty_Value_Is_Rejected()
    {
        Assert.Throws<ArgumentException>(
            () => new DocumentId(Guid.Empty));
    }

    [Fact]
    public void PageId_With_Empty_Value_Is_Rejected()
    {
        Assert.Throws<ArgumentException>(
            () => new PageId(Guid.Empty));
    }

    [Fact]
    public void ComponentId_New_Is_Not_Empty()
    {
        var id = ComponentId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void ReferenceId_New_Is_Not_Empty()
    {
        var id = ReferenceId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }
}
