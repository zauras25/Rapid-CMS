using RapidCMS.Domain.Compatibility;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public sealed class M1ArchitectureGuardTests
{
    [Fact]
    public void Native_Node_Identity_Is_Independent_From_Source_Identity()
    {
        var node = Node.Create("Button");

        Assert.NotEqual(Guid.Empty, node.Id.Value);
        Assert.Null(node.Compatibility.SourceIdentity);
        Assert.Equal(
            CompatibilityLevel.L0,
            node.Compatibility.Level);
    }

    [Fact]
    public void Native_Node_Identity_Differs_From_Source_Identity()
    {
        var node = Node.Create("Button");

        var sourceIdentity =
            SourceIdentity.Create(
                "figma",
                "123:456");

        node.SetCompatibility(
            CompatibilityMetadata.FromSource(
                sourceIdentity,
                CompatibilityLevel.L1));

        Assert.NotEqual(
            node.Id.ToString(),
            sourceIdentity.SourceId);

        Assert.Equal(
            "figma",
            node.Compatibility.SourceIdentity!.Source);

        Assert.Equal(
            "123:456",
            node.Compatibility.SourceIdentity.SourceId);
    }

    [Fact]
    public void Native_Entity_Defaults_To_L0_Without_Source()
    {
        var node = Node.Create("Native Node");

        Assert.Equal(
            CompatibilityLevel.L0,
            node.Compatibility.Level);

        Assert.Null(
            node.Compatibility.SourceIdentity);
    }

    [Fact]
    public void Compatibility_Metadata_Can_Preserve_Unsupported_Data()
    {
        var node = Node.Create("Unsupported Node");

        var sourceIdentity =
            SourceIdentity.Create(
                "figma",
                "unsupported-123");

        var compatibility =
            CompatibilityMetadata.FromSource(
                sourceIdentity,
                CompatibilityLevel.L4);

        compatibility.Preserve(
            "originalName",
            "Unsupported Node");

        node.SetCompatibility(compatibility);

        Assert.Equal(
            CompatibilityLevel.L4,
            node.Compatibility.Level);

        Assert.Equal(
            "Unsupported Node",
            node.Compatibility.PreservedData["originalName"]);
    }

    [Fact]
    public void Native_Ids_Are_Strongly_Typed()
    {
        var nodeId = NodeId.New();
        var referenceId = ReferenceId.New();

        Assert.NotEqual(nodeId.Value, referenceId.Value);
        Assert.Equal(typeof(NodeId), nodeId.GetType());
        Assert.Equal(typeof(ReferenceId), referenceId.GetType());
    }

    [Fact]
    public void Domain_Assembly_Contains_No_Figma_Types()
    {
        var assembly =
            typeof(Node).Assembly;

        var figmaTypes = assembly
            .GetTypes()
            .Where(type =>
                type.FullName?
                    .Contains(
                        "Figma",
                        StringComparison.OrdinalIgnoreCase)
                    == true)
            .ToList();

        Assert.Empty(figmaTypes);
    }
}
