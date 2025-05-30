using System.Collections.Generic;

public abstract class Node : INode
{
    protected NodeState _nodeState;
    public NodeState NodeState => _nodeState;

    public Node() { }

    public abstract NodeState Evaluate(); // 각 노드 타입에 따라 구현
}
