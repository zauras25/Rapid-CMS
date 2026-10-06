using System.Collections.ObjectModel;

namespace RapidCMS.Domain.Compatibility;

public sealed class CompatibilityMetadata
{
    private readonly Dictionary<string, string> _preservedData =
        new(StringComparer.Ordinal);

    private readonly Dictionary<string, string> _unsupportedData =
        new(StringComparer.Ordinal);

    public SourceIdentity? SourceIdentity { get; private set; }

    public CompatibilityLevel Level { get; private set; }

    public IReadOnlyDictionary<string, string> PreservedData =>
        new ReadOnlyDictionary<string, string>(_preservedData);

    public IReadOnlyDictionary<string, string> UnsupportedData =>
        new ReadOnlyDictionary<string, string>(_unsupportedData);

    private CompatibilityMetadata(
        CompatibilityLevel level,
        SourceIdentity? sourceIdentity)
    {
        Level = level;
        SourceIdentity = sourceIdentity;
    }

    public static CompatibilityMetadata Native() =>
        new(CompatibilityLevel.L0, null);

    public static CompatibilityMetadata FromSource(
        SourceIdentity sourceIdentity,
        CompatibilityLevel level)
    {
        ArgumentNullException.ThrowIfNull(sourceIdentity);

        return new CompatibilityMetadata(
            level,
            sourceIdentity);
    }

    public void ChangeLevel(CompatibilityLevel level)
    {
        Level = level;
    }

    public void Preserve(string key, string value)
    {
        ValidateKeyValue(key, value);

        _preservedData[key.Trim()] = value;
    }

    public void PreserveUnsupported(string key, string value)
    {
        ValidateKeyValue(key, value);

        _unsupportedData[key.Trim()] = value;
    }

    private static void ValidateKeyValue(
        string key,
        string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException(
                "Compatibility metadata key cannot be empty.",
                nameof(key));

        if (value is null)
            throw new ArgumentNullException(nameof(value));
    }
}
