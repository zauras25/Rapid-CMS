using System.Xml.Linq;
using Xunit;

namespace RapidCMS.Governance.Tests;

public sealed class M0ArchitectureGovernanceTests
{
    private static string RepositoryRoot
    {
        get
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null)
            {
                if (File.Exists(Path.Combine(directory.FullName, "Rapid-CMS.slnx")))
                    return directory.FullName;

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException(
                "Rapid-CMS repository root could not be located.");
        }
    }

    private static string ProjectPath(string folder)
    {
        return Directory
            .GetFiles(
                Path.Combine(RepositoryRoot, "src", folder),
                "*.csproj")
            .Single();
    }

    private static HashSet<string> GetReferences(string folder)
    {
        var path = ProjectPath(folder);

        var document = XDocument.Load(path);

        return document
            .Descendants("ProjectReference")
            .Select(x => Path.GetFileNameWithoutExtension(
                x.Attribute("Include")?.Value ?? string.Empty))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    private static void AssertOnlyAllowed(
        string project,
        HashSet<string> references,
        params string[] allowed)
    {
        var allowedSet = new HashSet<string>(
            allowed,
            StringComparer.OrdinalIgnoreCase);

        var forbidden = references
            .Where(reference => !allowedSet.Contains(reference))
            .ToArray();

        Assert.True(
            forbidden.Length == 0,
            $"{project} has forbidden project references: " +
            string.Join(", ", forbidden));
    }

    [Fact]
    public void Domain_Must_Have_No_Project_References()
    {
        var references = GetReferences("Domain");

        Assert.Empty(references);
    }

    [Fact]
    public void Engine_May_Depend_On_Domain_Only()
    {
        var references = GetReferences("Engine");

        AssertOnlyAllowed(
            "Engine",
            references,
            "RapidCMS.Domain");
    }

    [Fact]
    public void App_May_Depend_On_Domain_And_Contracts()
    {
        var references = GetReferences("App");

        AssertOnlyAllowed(
            "App",
            references,
            "RapidCMS.Domain",
            "RapidCMS.Contracts");
    }

    [Fact]
    public void Infra_May_Depend_On_Application_And_Domain()
    {
        var references = GetReferences("Infra");

        AssertOnlyAllowed(
            "Infra",
            references,
            "RapidCMS.Application",
            "RapidCMS.Domain");
    }

    [Fact]
    public void Api_May_Depend_On_Application_Contracts_And_Infrastructure()
    {
        var references = GetReferences("Api");

        AssertOnlyAllowed(
            "Api",
            references,
            "RapidCMS.Application",
            "RapidCMS.Contracts",
            "RapidCMS.Infrastructure");
    }

    [Fact]
    public void Domain_Must_Not_Depend_On_Higher_Layers()
    {
        var references = GetReferences("Domain");

        var forbidden = new[]
        {
            "RapidCMS.Application",
            "RapidCMS.Contracts",
            "RapidCMS.Engine",
            "RapidCMS.Infrastructure",
            "RapidCMS.Api"
        };

        foreach (var dependency in forbidden)
        {
            Assert.DoesNotContain(dependency, references);
        }
    }
}
