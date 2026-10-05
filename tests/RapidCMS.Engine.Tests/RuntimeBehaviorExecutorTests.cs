using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;
using RapidCMS.Engine.Behaviors;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeBehaviorExecutorTests
{
    [Fact]
    public void Execute_Runs_Behavior_On_Component()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.AddBehavior(
            ComponentBehavior.Create("test"));

        node.AttachComponent(component);

        var runtimeNode = RuntimeNode.From(node);

        var registry = new RuntimeBehaviorRegistry();

        var behavior = new TestBehavior("test");

        registry.Register(behavior);

        var executor = new RuntimeBehaviorExecutor(registry);
        var context = new RuntimeBehaviorContext();

        executor.Execute(runtimeNode, context);

        Assert.Equal(1, behavior.ExecutionCount);
        Assert.Same(runtimeNode, behavior.LastNode);
        Assert.Same(context, behavior.LastContext);
    }

    [Fact]
    public void Execute_Runs_Multiple_Behaviors_In_Component_Order()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.AddBehavior(
            ComponentBehavior.Create("first"));

        component.AddBehavior(
            ComponentBehavior.Create("second"));

        node.AttachComponent(component);

        var runtimeNode = RuntimeNode.From(node);

        var registry = new RuntimeBehaviorRegistry();

        var executionOrder = new List<string>();

        registry.Register(
            new TestBehavior(
                "first",
                () => executionOrder.Add("first")));

        registry.Register(
            new TestBehavior(
                "second",
                () => executionOrder.Add("second")));

        var executor = new RuntimeBehaviorExecutor(registry);

        executor.Execute(
            runtimeNode,
            new RuntimeBehaviorContext());

        Assert.Equal(
            new[] { "first", "second" },
            executionOrder);
    }

    [Fact]
    public void Execute_Does_Nothing_When_Node_Has_No_Component()
    {
        var node = Node.Create("Container");

        var runtimeNode = RuntimeNode.From(node);

        var registry = new RuntimeBehaviorRegistry();

        var executor = new RuntimeBehaviorExecutor(registry);

        var context = new RuntimeBehaviorContext();

        executor.Execute(runtimeNode, context);
    }

    [Fact]
    public void Execute_Throws_When_Behavior_Is_Not_Registered()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.AddBehavior(
            ComponentBehavior.Create("missing"));

        node.AttachComponent(component);

        var runtimeNode = RuntimeNode.From(node);

        var executor =
            new RuntimeBehaviorExecutor(
                new RuntimeBehaviorRegistry());

        Assert.Throws<InvalidOperationException>(
            () => executor.Execute(
                runtimeNode,
                new RuntimeBehaviorContext()));
    }

    [Fact]
    public void Execute_Passes_Context_To_Behavior()
    {
        var node = Node.Create("Button");

        var component = Component.Create(
            node.Id,
            ComponentType.Create("Button"));

        component.AddBehavior(
            ComponentBehavior.Create("test"));

        node.AttachComponent(component);

        var runtimeNode = RuntimeNode.From(node);

        var registry = new RuntimeBehaviorRegistry();

        registry.Register(
            new TestBehavior(
                "test",
                context => context.Set("executed", true)));

        var executor = new RuntimeBehaviorExecutor(registry);

        var context = new RuntimeBehaviorContext();

        executor.Execute(runtimeNode, context);

        Assert.True(
            context.TryGet(
                "executed",
                out var value));

        Assert.Equal(true, value);
    }

    private sealed class TestBehavior : IRuntimeBehavior
    {
        private readonly Action? _action;
        private readonly Action<RuntimeBehaviorContext>? _contextAction;

        public string Name { get; }

        public int ExecutionCount { get; private set; }

        public RuntimeNode? LastNode { get; private set; }

        public RuntimeBehaviorContext? LastContext { get; private set; }

        public TestBehavior(
            string name,
            Action? action = null)
        {
            Name = name;
            _action = action;
        }

        public TestBehavior(
            string name,
            Action<RuntimeBehaviorContext> contextAction)
        {
            Name = name;
            _contextAction = contextAction;
        }

        public void Execute(
            RuntimeNode node,
            RuntimeBehaviorContext context)
        {
            ExecutionCount++;
            LastNode = node;
            LastContext = context;

            _action?.Invoke();
            _contextAction?.Invoke(context);
        }
    }
}
