using System.Xml.Linq;

namespace RapidCMS.Domain.Tests;

public sealed class M1DependencyBoundaryTests
{
    private static string RepositoryRoot
    {
        get
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory is not null)
            {
                var domainProject = Path.Combine(
                    directory.FullName,
                    "src",
                    "Domain",
                    "RapidCMS.Domain.csproj");

                var engineProject = Path.Combine(
                    directory.FullName,
                    "src",
                    "Engine",
                    "Engine.csproj");

                if (File.Exists(domainProject) &&
                    File.Exists(engineProject))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            throw new InvalidOperationException(
                "Repository root could not be located.");
        }
    }

    [Fact]
    public void Domain_Project_Has_No_Project_References()
    {
        var projectPath = Path.Combine(
            RepositoryRoot,
            "src",
            "Domain",
            "RapidCMS.Domain.csproj");

        var document = XDocument.Load(projectPath);

        var references = document
            .Descendants("ProjectReference")
            .Select(x => (string?)x.Attribute("Include"))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        Assert.Empty(references);
    }

    [Fact]
    public void Domain_Project_Has_No_Figma_Package_References()
    {
        var projectPath = Path.Combine(
            RepositoryRoot,
            "src",
            "Domain",
            "RapidCMS.Domain.csproj");

        var document = XDocument.Load(projectPath);

        var packages = document
            .Descendants("PackageReference")
            .Select(x => (string?)x.Attribute("Include"))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        Assert.DoesNotContain(
            packages,
            package =>
                package!.Contains(
                    "Figma",
                    StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Engine_Project_Depends_Only_On_Domain()
    {
        var projectPath = Path.Combine(
            RepositoryRoot,
            "src",
            "Engine",
            "Engine.csproj");

        var document = XDocument.Load(projectPath);

        var references = document
            .Descendants("ProjectReference")
            .Select(x => (string?)x.Attribute("Include"))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        Assert.Single(references);

        Assert.Contains(
            references,
            reference =>
                reference!.Replace('\\', '/')
                    .EndsWith(
                        "/Domain/RapidCMS.Domain.csproj",
                        StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Core_Source_Code_Contains_No_Figma_References()
    {
        var sourceDirectories = new[]
        {
            Path.Combine(RepositoryRoot, "src", "Domain"),
            Path.Combine(RepositoryRoot, "src", "Engine")
        };

        var files = sourceDirectories
            .Where(Directory.Exists)
            .SelectMany(directory =>
                Directory.GetFiles(
                    directory,
                    "*.cs",
                    SearchOption.AllDirectories))
            .Where(file =>
                !file.Contains(
                    $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}",
                    StringComparison.OrdinalIgnoreCase) &&
                !file.Contains(
                    $"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}",
                    StringComparison.OrdinalIgnoreCase))
            .ToList();

        var forbiddenFiles = files
            .Where(file =>
                File.ReadAllText(file)
                    .Contains(
                        "Figma",
                        StringComparison.OrdinalIgnoreCase))
            .ToList();

        Assert.Empty(forbiddenFiles);
    }

    [Fact]
    public void Core_Source_Code_Contains_No_Figma_Sdk_References()
    {
        var sourceDirectories = new[]
        {
            Path.Combine(RepositoryRoot, "src", "Domain"),
            Path.Combine(RepositoryRoot, "src", "Engine")
        };

        var forbiddenTerms = new[]
        {
            "FigmaApi",
            "FigmaSdk",
            "FigmaClient",
            "FigmaRest",
            "FigmaPlugin"
        };

        var files = sourceDirectories
            .Where(Directory.Exists)
            .SelectMany(directory =>
                Directory.GetFiles(
                    directory,
                    "*.cs",
                    SearchOption.AllDirectories))
            .Where(file =>
                !file.Contains(
                    $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}",
                    StringComparison.OrdinalIgnoreCase) &&
                !file.Contains(
                    $"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}",
                    StringComparison.OrdinalIgnoreCase))
            .ToList();

        var violations = new List<string>();

        foreach (var file in files)
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

        Assert.Empty(violations);
    }
}

