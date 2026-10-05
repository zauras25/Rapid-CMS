namespace RapidCMS.Contracts.Commands;

public sealed record CommandResult<TResult>(
    bool Succeeded,
    TResult? Data,
    IReadOnlyList<string> Errors)
{
    public static CommandResult<TResult> Success(TResult data) =>
        new(true, data, Array.Empty<string>());

    public static CommandResult<TResult> Failure(params string[] errors) =>
        new(false, default, errors);
}
