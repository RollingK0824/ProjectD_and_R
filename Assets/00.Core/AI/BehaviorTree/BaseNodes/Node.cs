using System.Collections.Generic;

public abstract class Node : INode
{
    protected NodeState _nodeState;
    public NodeState nodeState { get => _nodeState; }

    protected List<INode> _children = new List<INode>(); // 자식 노드 리스트

    public Node() { }

    public Node(List<INode> children)
    {
        _children = children;
    }

    // 자식 노드를 추가하는 메서드 (복합 노드에서 주로 사용)
    public void AddChild(INode child)
    {
        _children.Add(child);
    }

    public abstract NodeState Evaluate(); // 각 노드 타입에 따라 구현
}
