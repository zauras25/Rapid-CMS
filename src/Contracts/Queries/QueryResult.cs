namespace RapidCMS.Contracts.Queries;

public sealed record QueryResult<TResult>(
    bool Succeeded,
    TResult? Data,
    IReadOnlyList<string> Errors)
{
    public static QueryResult<TResult> Success(TResult data) =>
        new(true, data, Array.Empty<string>());

    public static QueryResult<TResult> Failure(params string[] errors) =>
        new(false, default, errors);
}
