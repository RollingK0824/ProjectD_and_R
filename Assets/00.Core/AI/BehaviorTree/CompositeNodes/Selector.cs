using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public Selector() : base() { }
    public Selector(List<INode> children) : base(children) { }

    public override NodeState Evaluate()
    {
        foreach (var child in _children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    return _nodeState;
                case NodeState.Success:
                    _nodeState = NodeState.Success;
                    return _nodeState;
                case NodeState.Failure:
                    continue;
                default:
                    break;
            }
        }

        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}
