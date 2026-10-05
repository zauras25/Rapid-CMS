namespace RapidCMS.Engine.Behaviors;

public sealed class RuntimeBehaviorContext
{
    private readonly Dictionary<string, object?> _values = new();

    public IReadOnlyDictionary<string, object?> Values => _values;

    public void Set(string key, object? value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException(
                "Context key cannot be empty.",
                nameof(key));

        _values[key.Trim()] = value;
    }

    public bool TryGet(
        string key,
        out object? value)
    {
        return _values.TryGetValue(key, out value);
    }
}
