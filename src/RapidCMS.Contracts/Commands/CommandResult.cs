namespace RapidCMS.Contracts.Commands;

public sealed record CommandResult(
    bool Succeeded,
    IReadOnlyList<string> Errors)
{
    public static CommandResult Success() =>
        new(true, Array.Empty<string>());

    public static CommandResult Failure(params string[] errors) =>
        new(false, errors);
}
