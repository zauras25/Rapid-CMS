using Xunit;

namespace RapidCMS.Governance.Tests;

public sealed class M2SourceBoundaryTests
{
    private static string RepositoryRoot
    {
        get
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory is not null)
            {
                if (File.Exists(
                    Path.Combine(
                        directory.FullName,
                        "Rapid-CMS.slnx")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "RapidCMS repository root could not be located.");
        }
    }

    private static IEnumerable<string> SourceFiles(string folder)
    {
        var path = Path.Combine(
            RepositoryRoot,
            "src",
            folder);

        return Directory.Exists(path)
            ? Directory.GetFiles(
                path,
                "*.cs",
                SearchOption.AllDirectories)
                .Where(file =>
                    !file.Contains(
                        $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}",
                        StringComparison.OrdinalIgnoreCase) &&
                    !file.Contains(
                        $"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}",
                        StringComparison.OrdinalIgnoreCase))
            : Enumerable.Empty<string>();
    }

    private static void AssertSourceDoesNotContain(
        string folder,
        params string[] forbiddenTerms)
    {
        var violations = new List<string>();

        foreach (var file in SourceFiles(folder))
        {
            var content = File.ReadAllText(file);

            foreach (var term in forbiddenTerms)
            {
                if (content.Contains(
                    term,
                    StringComparison.OrdinalIgnoreCase))
                {
                    violations.Add(
                        $"{Path.GetFileName(file)} -> {term}");
                }
            }
        }

        Assert.True(
            violations.Count == 0,
            $"{folder} contains forbidden source references:" +
            Environment.NewLine +
            string.Join(
                Environment.NewLine,
                violations));
    }

    [Fact]
    public void Domain_Source_Must_Not_Reference_Higher_Layers()
    {
        AssertSourceDoesNotContain(
            "Domain",
            "RapidCMS.Application",
            "RapidCMS.Contracts",
            "RapidCMS.Infrastructure",
            "RapidCMS.Api",
            "FigmaApi",
            "FigmaSdk",
            "FigmaClient");
    }

    [Fact]
    public void Contracts_Source_Must_Remain_Independent()
    {
        AssertSourceDoesNotContain(
            "Contracts",
            "RapidCMS.Domain",
            "RapidCMS.Application",
            "RapidCMS.Infrastructure",
            "RapidCMS.Api",
            "FigmaApi",
            "FigmaSdk",
            "FigmaClient");
    }

    [Fact]
    public void Engine_Source_Must_Remain_Domain_Bound()
    {
        AssertSourceDoesNotContain(
            "Engine",
            "RapidCMS.Application",
            "RapidCMS.Contracts",
            "RapidCMS.Infrastructure",
            "RapidCMS.Api",
            "FigmaApi",
            "FigmaSdk",
            "FigmaClient");
    }

    [Fact]
    public void Application_Source_Must_Not_Reference_Infrastructure_Or_Api()
    {
        AssertSourceDoesNotContain(
            "App",
            "RapidCMS.Infrastructure",
            "RapidCMS.Api",
            "FigmaApi",
            "FigmaSdk",
            "FigmaClient");
    }

    [Fact]
    public void Infrastructure_Source_Must_Not_Reference_Api()
    {
        AssertSourceDoesNotContain(
            "Infra",
            "RapidCMS.Api",
            "FigmaApi",
            "FigmaSdk",
            "FigmaClient");
    }
}
