using RapidCMS.Domain.Documents;
using RapidCMS.Domain.References;

namespace RapidCMS.Domain.Tests;

public class DocumentReferenceTests
{
    [Fact]
    public void Document_Can_Add_And_Remove_Reference()
    {
        var document = Document.Create();

        var reference = DocumentReference.Create(
            "Node",
            Guid.NewGuid(),
            "Asset",
            Guid.NewGuid(),
            "Uses");

        document.AddReference(reference.Id);

        Assert.Contains(
            reference.Id,
            document.ReferenceIds);

        document.RemoveReference(reference.Id);

        Assert.DoesNotContain(
            reference.Id,
            document.ReferenceIds);
    }

    [Fact]
    public void Document_Cannot_Add_Duplicate_Reference()
    {
        var document = Document.Create();

        var reference = DocumentReference.Create(
            "Node",
            Guid.NewGuid(),
            "Asset",
            Guid.NewGuid(),
            "Uses");

        document.AddReference(reference.Id);

        Assert.Throws<InvalidOperationException>(
            () => document.AddReference(reference.Id));
    }

    [Fact]
    public void Document_Cannot_Add_Empty_Reference_Id()
    {
        var document = Document.Create();

        Assert.Throws<ArgumentException>(
            () => document.AddReference(
                new RapidCMS.Domain.Identity.ReferenceId(
                    Guid.Empty)));
    }
}
