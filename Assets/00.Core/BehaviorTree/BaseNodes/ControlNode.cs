using System.Collections.Generic;

public abstract class ControlNode : Node
{
    protected List<INode> _children = new List<INode>();

    public ControlNode() : base() { }

    public ControlNode(INode child) : base()
    {
        _children.Add(child);
    }

    public ControlNode(List<INode> children) : base()
    {
        _children = children;
    }

    public void AddChild(INode child)
    {
        if(child != null)
        {
            _children.Add(child);
        }
    }

    public IReadOnlyList<INode> GetChildren()
    {
        return _children.AsReadOnly();
    }

}
