using RapidCMS.Domain.Documents;
using RapidCMS.Domain.Variables;

namespace RapidCMS.Domain.Tests;

public class DocumentVariableTests
{
    [Fact]
    public void Document_Can_Add_And_Remove_Variable()
    {
        var document = Document.Create();

        var variable = Variable.Create(
            "primary-color",
            "#2563EB");

        document.AddVariable(variable.Id);

        Assert.Contains(
            variable.Id,
            document.VariableIds);

        document.RemoveVariable(variable.Id);

        Assert.DoesNotContain(
            variable.Id,
            document.VariableIds);
    }

    [Fact]
    public void Document_Cannot_Add_Duplicate_Variable()
    {
        var document = Document.Create();

        var variable = Variable.Create(
            "primary-color",
            "#2563EB");

        document.AddVariable(variable.Id);

        Assert.Throws<InvalidOperationException>(
            () => document.AddVariable(variable.Id));
    }

    [Fact]
    public void Document_Cannot_Add_Empty_Variable_Id()
    {
        var document = Document.Create();

        Assert.Throws<ArgumentException>(
            () => document.AddVariable(
                new RapidCMS.Domain.Identity.VariableId(
                    Guid.Empty)));
    }
}
