using RapidCMS.Domain.Variables;

namespace RapidCMS.Domain.Tests;

public class VariableTests
{
    [Fact]
    public void Variable_Can_Be_Created()
    {
        var variable = Variable.Create(
            "primary-color",
            "#2563EB");

        Assert.NotEqual(Guid.Empty, variable.Id.Value);
        Assert.Equal("primary-color", variable.Name);
        Assert.Equal("#2563EB", variable.Value);
    }

    [Fact]
    public void Variable_Can_Be_Renamed()
    {
        var variable = Variable.Create(
            "primary-color",
            "#2563EB");

        variable.Rename("brand-color");

        Assert.Equal("brand-color", variable.Name);
    }

    [Fact]
    public void Variable_Can_Change_Value()
    {
        var variable = Variable.Create(
            "primary-color",
            "#2563EB");

        variable.SetValue("#16A34A");

        Assert.Equal("#16A34A", variable.Value);
    }
}
