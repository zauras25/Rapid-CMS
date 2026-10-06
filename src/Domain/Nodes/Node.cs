using RapidCMS.Domain.Common;
using RapidCMS.Domain.Components;
using RapidCMS.Domain.Identity;
using RapidCMS.Domain.Styles;

namespace RapidCMS.Domain.Nodes;

public sealed class Node : Entity<NodeId>
{
    private readonly List<NodeId> _children = new();
    private Node? _parent;

    public string Name { get; private set; }

    public NodeId? ParentId => _parent?.Id;

    public Component? Component { get; private set; }

    public Style? Style { get; private set; }

    public IReadOnlyList<NodeId> Children => _children;

    private Node(NodeId id, string name)
        : base(id)
    {
        Name = name;
    }

    public static Node Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Node name cannot be empty.",
                nameof(name));

        return new Node(NodeId.New(), name.Trim());
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Node name cannot be empty.",
                nameof(name));

        Name = name.Trim();
    }

    public void AttachComponent(Component component)
    {
        ArgumentNullException.ThrowIfNull(component);

        if (component.Id != Id)
            throw new InvalidOperationException(
                "Component must belong to this node.");

        if (Component is not null)
            throw new InvalidOperationException(
                "Node already has a component.");

        Component = component;
    }

    public void RemoveComponent()
    {
        Component = null;
    }

    public void AttachStyle(Style style)
    {
        ArgumentNullException.ThrowIfNull(style);

        if (style.Id != Id)
            throw new InvalidOperationException(
                "Style must belong to this node.");

        if (Style is not null)
            throw new InvalidOperationException(
                "Node already has a style.");

        Style = style;
    }

    public void RemoveStyle()
    {
        Style = null;
    }

    public void AddChild(Node child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (child.Id == Id)
            throw new InvalidOperationException(
                "A node cannot be its own child.");

        if (_children.Contains(child.Id))
            throw new InvalidOperationException(
                $"Node '{child.Id}' is already a child.");

        if (child._parent is not null)
            throw new InvalidOperationException(
                "Node already has another parent.");

        if (IsAncestorOf(child))
            throw new InvalidOperationException(
                "Adding this child would create a cycle.");

        _children.Add(child.Id);
        child._parent = this;
    }

    public void RemoveChild(Node child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (_children.Remove(child.Id) && child._parent == this)
            child._parent = null;
    }

    public void MoveTo(Node? newParent)
    {
        if (newParent is null)
        {
            _parent = null;
            return;
        }

        if (newParent.Id == Id)
            throw new InvalidOperationException(
                "A node cannot be its own parent.");

        if (newParent.IsAncestorOf(this))
            throw new InvalidOperationException(
                "Moving this node would create a cycle.");

        if (_parent == newParent)
            return;

        _parent?._children.Remove(Id);

        if (newParent._children.Contains(Id))
            return;

        if (_parent is not null)
            _parent = null;

        newParent._children.Add(Id);
        _parent = newParent;
    }


    public static Node Rehydrate(
        NodeId id,
        string name,
        NodeId? parentId = null,
        IEnumerable<NodeId>? children = null)
    {
        if (id.Value == Guid.Empty)
            throw new ArgumentException(
                "Node ID cannot be empty.",
                nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Node name cannot be empty.",
                nameof(name));

        var node = new Node(id, name.Trim());

        if (children is not null)
        {
            foreach (var childId in children)
            {
                if (childId == id)
                    throw new InvalidOperationException(
                        "A node cannot contain itself as a child.");

                if (!node._children.Contains(childId))
                    node._children.Add(childId);
            }
        }

        return node;
    }

    public void RestoreChild(Node child)
    {
        ArgumentNullException.ThrowIfNull(child);

        if (child.Id == Id)
            throw new InvalidOperationException(
                "A node cannot be its own child.");

        if (_children.Contains(child.Id))
            return;

        _children.Add(child.Id);
        child._parent = this;
    }

    public void RestoreParent(Node parent)
    {
        ArgumentNullException.ThrowIfNull(parent);

        if (parent.Id == Id)
            throw new InvalidOperationException(
                "A node cannot be its own parent.");

        _parent = parent;
    }
    private bool IsAncestorOf(Node candidate)
    {
        var current = _parent;

        while (current is not null)
        {
            if (current.Id == candidate.Id)
                return true;

            current = current._parent;
        }

        return false;
    }
}




