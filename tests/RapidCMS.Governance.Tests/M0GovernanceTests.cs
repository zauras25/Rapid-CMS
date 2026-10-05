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
        var path = GovernancePath("M0-Acceptance-Matrix.md");

        Assert.True(
            File.Exists(path),
            $"M0 Acceptance Matrix is missing: {path}");
    }

    [Fact]
    public void M0_Acceptance_Record_Must_Exist()
    {
        var path = GovernancePath("M0-Acceptance-Record.md");

        Assert.True(
            File.Exists(path),
            $"M0 Acceptance Record is missing: {path}");
    }

    [Fact]
    public void M0_Freeze_Record_Must_Exist()
    {
        var path = GovernancePath("M0-Freeze-Record.md");

        Assert.True(
            File.Exists(path),
            $"M0 Freeze Record is missing: {path}");
    }

    [Fact]
    public void M0_Cannot_Be_Accepted_While_Mandatory_Criteria_Are_Pending()
    {
        var matrix = File.ReadAllText(
            GovernancePath("M0-Acceptance-Matrix.md"));

        var acceptanceRecord = File.ReadAllText(
            GovernancePath("M0-Acceptance-Record.md"));

        var hasPendingCriteria =
            matrix.Contains(
                "PENDING",
                StringComparison.OrdinalIgnoreCase);

        var isAccepted =
            acceptanceRecord.Contains(
                "Status: ACCEPTED",
                StringComparison.OrdinalIgnoreCase);

        Assert.False(
            hasPendingCriteria && isAccepted,
            "M0 cannot be ACCEPTED while mandatory acceptance criteria are PENDING.");
    }

    [Fact]
    public void M0_Cannot_Be_Frozen_Without_Acceptance()
    {
        var acceptanceRecord = File.ReadAllText(
            GovernancePath("M0-Acceptance-Record.md"));

        var freezeRecord = File.ReadAllText(
            GovernancePath("M0-Freeze-Record.md"));

        var isAccepted =
            acceptanceRecord.Contains(
                "Status: ACCEPTED",
                StringComparison.OrdinalIgnoreCase);

        var isFrozen =
            freezeRecord.Contains(
                "Status: FROZEN",
                StringComparison.OrdinalIgnoreCase);

        Assert.False(
            isFrozen && !isAccepted,
            "M0 cannot be FROZEN unless M0 is ACCEPTED.");
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
    public void M0_Must_Not_Be_Frozen_Currently()
    {
        var freezeRecord = File.ReadAllText(
            GovernancePath("M0-Freeze-Record.md"));

        Assert.Contains(
            "Status: NOT FROZEN",
            freezeRecord,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void M0_Must_Not_Be_Accepted_Currently()
    {
        var acceptanceRecord = File.ReadAllText(
            GovernancePath("M0-Acceptance-Record.md"));

        Assert.Contains(
            "Status: PENDING",
            acceptanceRecord,
            StringComparison.OrdinalIgnoreCase);
    }
}
