using RapidCMS.Engine.Behaviors;
using RapidCMS.Engine.Runtime;
using RapidCMS.Domain.Nodes;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeBehaviorRegistryTests
{
    [Fact]
    public void Registry_Registers_And_Resolves_Behavior()
    {
        var registry = new RuntimeBehaviorRegistry();

        var behavior = new TestBehavior("Navigate");

        registry.Register(behavior);

        var resolved = registry.Resolve("Navigate");

        Assert.Same(behavior, resolved);
    }

    [Fact]
    public void Registry_Is_Case_Insensitive()
    {
        var registry = new RuntimeBehaviorRegistry();

        var behavior = new TestBehavior("Navigate");

        registry.Register(behavior);

        Assert.True(
            registry.TryResolve(
                "navigate",
                out var resolved));

        Assert.Same(behavior, resolved);
    }

    [Fact]
    public void Registry_Rejects_Duplicate_Behavior()
    {
        var registry = new RuntimeBehaviorRegistry();

        registry.Register(
            new TestBehavior("Navigate"));

        Assert.Throws<InvalidOperationException>(
            () => registry.Register(
                new TestBehavior("Navigate")));
    }

    [Fact]
    public void Registry_Rejects_Missing_Behavior()
    {
        var registry = new RuntimeBehaviorRegistry();

        Assert.Throws<InvalidOperationException>(
            () => registry.Resolve("Missing"));
    }

    [Fact]
    public void Context_Stores_Values()
    {
        var context = new RuntimeBehaviorContext();

        context.Set("target", "button");

        Assert.True(
            context.TryGet(
                "target",
                out var value));

        Assert.Equal("button", value);
    }

    private sealed class TestBehavior : IRuntimeBehavior
    {
        public string Name { get; }

        public TestBehavior(string name)
        {
            Name = name;
        }

        public void Execute(
            RuntimeNode node,
            RuntimeBehaviorContext context)
        {
        }
    }
}
