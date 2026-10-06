using RapidCMS.Domain.Compatibility;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Domain.Tests;

public sealed class CompatibilityMetadataTests
{
    [Fact]
    public void New_Native_Node_Uses_L0_And_Has_No_Source_Identity()
    {
        var node = Node.Create("Button");

        Assert.Equal(
            CompatibilityLevel.L0,
            node.Compatibility.Level);

        Assert.Null(
            node.Compatibility.SourceIdentity);
    }

    [Fact]
    public void Source_Identity_Is_Separate_From_Native_Identity()
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
            Guid.Empty,
            node.Id.Value);

        Assert.Equal(
            "figma",
            node.Compatibility.SourceIdentity!.Source);

        Assert.Equal(
            "123:456",
            node.Compatibility.SourceIdentity.SourceId);
    }

    [Fact]
    public void Unsupported_Data_Is_Preserved()
    {
        var metadata =
            CompatibilityMetadata.FromSource(
                SourceIdentity.Create(
                    "figma",
                    "123:456"),
                CompatibilityLevel.L4);

        metadata.PreserveUnsupported(
            "prototypeProperty",
            """{"value":"preserve-me"}""");

        Assert.Equal(
            """{"value":"preserve-me"}""",
            metadata.UnsupportedData["prototypeProperty"]);
    }

    [Fact]
    public void Preserved_Data_Is_Not_The_Core_Node_Model()
    {
        var node = Node.Create("Button");

        node.Compatibility.Preserve(
            "originalName",
            "Figma Button");

        Assert.Equal(
            "Button",
            node.Name);

        Assert.Equal(
            "Figma Button",
            node.Compatibility.PreservedData["originalName"]);
    }
}
