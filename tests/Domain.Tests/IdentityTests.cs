using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Tests;

public class IdentityTests
{
    [Fact]
    public void All_Native_Ids_Generate_NonEmpty_Values()
    {
        Assert.NotEqual(Guid.Empty, ProjectId.New().Value);
        Assert.NotEqual(Guid.Empty, DocumentId.New().Value);
        Assert.NotEqual(Guid.Empty, PageId.New().Value);
        Assert.NotEqual(Guid.Empty, NodeId.New().Value);
        Assert.NotEqual(Guid.Empty, AssetId.New().Value);
        Assert.NotEqual(Guid.Empty, ComponentId.New().Value);
        Assert.NotEqual(Guid.Empty, ComponentSetId.New().Value);
        Assert.NotEqual(Guid.Empty, InstanceId.New().Value);
        Assert.NotEqual(Guid.Empty, InteractionId.New().Value);
        Assert.NotEqual(Guid.Empty, PrototypeId.New().Value);
        Assert.NotEqual(Guid.Empty, ReferenceId.New().Value);
        Assert.NotEqual(Guid.Empty, StyleId.New().Value);
        Assert.NotEqual(Guid.Empty, TokenId.New().Value);
        Assert.NotEqual(Guid.Empty, VariableId.New().Value);
        Assert.NotEqual(Guid.Empty, VariantId.New().Value);
    }

    [Fact]
    public void ProjectId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new ProjectId(Guid.Empty));
    }

    [Fact]
    public void AssetId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new AssetId(Guid.Empty));
    }

    [Fact]
    public void ComponentId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new ComponentId(Guid.Empty));
    }

    [Fact]
    public void InstanceId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new InstanceId(Guid.Empty));
    }

    [Fact]
    public void InteractionId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new InteractionId(Guid.Empty));
    }

    [Fact]
    public void TokenId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new TokenId(Guid.Empty));
    }

    [Fact]
    public void VariantId_Rejects_Empty_Guid()
    {
        Assert.Throws<ArgumentException>(
            () => new VariantId(Guid.Empty));
    }
}
