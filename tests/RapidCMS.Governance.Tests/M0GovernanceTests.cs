using Xunit;

namespace RapidCMS.Governance.Tests;

public sealed class M0GovernanceTests
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

    private static string GovernancePath(string fileName)
    {
        return Path.Combine(
            RepositoryRoot,
            "docs",
            "governance",
            "milestones",
            "M0",
            fileName);
    }

    [Fact]
    public void M0_Acceptance_Matrix_Must_Exist()
    {
        Assert.True(
            File.Exists(GovernancePath("M0-Acceptance-Matrix.md")));
    }

    [Fact]
    public void M0_Acceptance_Record_Must_Exist()
    {
        Assert.True(
            File.Exists(GovernancePath("M0-Acceptance-Record.md")));
    }

    [Fact]
    public void M0_Freeze_Record_Must_Exist()
    {
        Assert.True(
            File.Exists(GovernancePath("M0-Freeze-Record.md")));
    }

    [Fact]
    public void M0_Acceptance_Matrix_Must_Have_No_Pending_Criteria()
    {
        var matrix = File.ReadAllText(
            GovernancePath("M0-Acceptance-Matrix.md"));

        Assert.DoesNotContain(
            "PENDING",
            matrix,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void M0_Must_Be_Accepted()
    {
        var acceptanceRecord = File.ReadAllText(
            GovernancePath("M0-Acceptance-Record.md"));

        Assert.Contains(
            "Status: ACCEPTED",
            acceptanceRecord,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void M0_Must_Be_Frozen()
    {
        var freezeRecord = File.ReadAllText(
            GovernancePath("M0-Freeze-Record.md"));

        Assert.Contains(
            "Status: FROZEN",
            freezeRecord,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void M0_Freeze_Record_Must_Require_Acceptance()
    {
        var freezeRecord = File.ReadAllText(
            GovernancePath("M0-Freeze-Record.md"));

        Assert.Contains(
            "M0 Acceptance Status = ACCEPTED",
            freezeRecord,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void M0_Freeze_Must_Reference_Accepted_Baseline()
    {
        var acceptanceRecord = File.ReadAllText(
            GovernancePath("M0-Acceptance-Record.md"));

        var freezeRecord = File.ReadAllText(
            GovernancePath("M0-Freeze-Record.md"));

        Assert.Contains(
            "Status: ACCEPTED",
            acceptanceRecord,
            StringComparison.OrdinalIgnoreCase);

        Assert.Contains(
            "Status: FROZEN",
            freezeRecord,
            StringComparison.OrdinalIgnoreCase);
    }
}
