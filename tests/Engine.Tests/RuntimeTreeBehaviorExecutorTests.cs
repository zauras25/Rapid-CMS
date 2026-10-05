using RapidCMS.Domain.Components;
using RapidCMS.Domain.Nodes;
using RapidCMS.Engine.Behaviors;
using RapidCMS.Engine.Runtime;

namespace RapidCMS.Engine.Tests;

public sealed class RuntimeTreeBehaviorExecutorTests
{
    [Fact]
    public void Execute_Runs_Behaviors_For_Entire_Tree()
    {
        var root = CreateNode("Root", "root");
        var child = CreateNode("Child", "child");
        var grandchild = CreateNode("Grandchild", "grandchild");

        root.AddChild(child);
        child.AddChild(grandchild);

        var nodes = new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>
        {
            [root.Id] = root,
            [child.Id] = child,
            [grandchild.Id] = grandchild
        };

        var tree = RuntimeTree.From(root, nodes);

        var executionOrder = new List<string>();

        var registry = new RuntimeBehaviorRegistry();

        registry.Register(
            new RecordingBehavior(
                "root",
                executionOrder));

        registry.Register(
            new RecordingBehavior(
                "child",
                executionOrder));

        registry.Register(
            new RecordingBehavior(
                "grandchild",
                executionOrder));

        var executor = new RuntimeBehaviorExecutor(registry);

        var treeExecutor =
            new RuntimeTreeBehaviorExecutor(executor);

        treeExecutor.Execute(
            tree,
            new RuntimeBehaviorContext());

        Assert.Equal(
            new[]
            {
                "root",
                "child",
                "grandchild"
            },
            executionOrder);
    }

    [Fact]
    public void Execute_Uses_PreOrder_Traversal()
    {
        var root = CreateNode("Root", "root");
        var first = CreateNode("First", "first");
        var second = CreateNode("Second", "second");
        var firstChild = CreateNode("FirstChild", "first-child");

        root.AddChild(first);
        root.AddChild(second);
        first.AddChild(firstChild);

        var nodes = new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>
        {
            [root.Id] = root,
            [first.Id] = first,
            [second.Id] = second,
            [firstChild.Id] = firstChild
        };

        var tree = RuntimeTree.From(root, nodes);

        var executionOrder = new List<string>();

        var registry = new RuntimeBehaviorRegistry();

        foreach (var name in new[]
        {
            "root",
            "first",
            "second",
            "first-child"
        })
        {
            registry.Register(
                new RecordingBehavior(
                    name,
                    executionOrder));
        }

        var treeExecutor =
            new RuntimeTreeBehaviorExecutor(
                new RuntimeBehaviorExecutor(registry));

        treeExecutor.Execute(
            tree,
            new RuntimeBehaviorContext());

        Assert.Equal(
            new[]
            {
                "root",
                "first",
                "first-child",
                "second"
            },
            executionOrder);
    }

    [Fact]
    public void Execute_Shares_Same_Context_Across_Tree()
    {
        var root = CreateNode("Root", "root");
        var child = CreateNode("Child", "child");

        root.AddChild(child);

        var nodes = new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>
        {
            [root.Id] = root,
            [child.Id] = child
        };

        var tree = RuntimeTree.From(root, nodes);

        var registry = new RuntimeBehaviorRegistry();

        registry.Register(
            new RecordingBehavior(
                "root",
                context => context.Set("root", true)));

        registry.Register(
            new RecordingBehavior(
                "child",
                context =>
                {
                    Assert.True(
                        context.TryGet(
                            "root",
                            out var value));

                    Assert.Equal(true, value);

                    context.Set("child", true);
                }));

        var context = new RuntimeBehaviorContext();

        var treeExecutor =
            new RuntimeTreeBehaviorExecutor(
                new RuntimeBehaviorExecutor(registry));

        treeExecutor.Execute(tree, context);

        Assert.True(
            context.TryGet(
                "child",
                out var childValue));

        Assert.Equal(true, childValue);
    }

    [Fact]
    public void Execute_Does_Nothing_For_Tree_Without_Behaviors()
    {
        var root = Node.Create("Container");

        var nodes = new Dictionary<RapidCMS.Domain.Identity.NodeId, Node>
        {
            [root.Id] = root
        };

        var tree = RuntimeTree.From(root, nodes);

        var executor =
            new RuntimeTreeBehaviorExecutor(
                new RuntimeBehaviorExecutor(
                    new RuntimeBehaviorRegistry()));

        executor.Execute(
            tree,
            new RuntimeBehaviorContext());
    }

    private static Node CreateNode(
        string nodeName,
        string behaviorName)
    {
        var node = Node.Create(nodeName);

        var component = Component.Create(
            node.Id,
            ComponentType.Create(nodeName));

        component.AddBehavior(
            ComponentBehavior.Create(behaviorName));

        node.AttachComponent(component);

        return node;
    }

    private sealed class RecordingBehavior : IRuntimeBehavior
    {
        private readonly List<string>? _executionOrder;
        private readonly Action<RuntimeBehaviorContext>? _action;

        public string Name { get; }

        public RecordingBehavior(
            string name,
            List<string> executionOrder)
        {
            Name = name;
            _executionOrder = executionOrder;
        }

        public RecordingBehavior(
            string name,
            Action<RuntimeBehaviorContext> action)
        {
            Name = name;
            _action = action;
        }

        public void Execute(
            RuntimeNode node,
            RuntimeBehaviorContext context)
        {
            _executionOrder?.Add(Name);
            _action?.Invoke(context);
        }
    }
}
