using RapidCMS.Domain.Common;
using RapidCMS.Domain.Identity;

namespace RapidCMS.Domain.Variables;

public sealed class Variable : Entity<VariableId>
{
    public string Name { get; private set; }

    public string Value { get; private set; }

    private Variable(
        VariableId id,
        string name,
        string value)
        : base(id)
    {
        Name = name;
        Value = value;
    }

    public static Variable Create(
        string name,
        string value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Variable name cannot be empty.",
                nameof(name));

        return new Variable(
            VariableId.New(),
            name.Trim(),
            value ?? string.Empty);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Variable name cannot be empty.",
                nameof(name));

        Name = name.Trim();
    }

    public void SetValue(string value)
    {
        Value = value ?? string.Empty;
    }
}
