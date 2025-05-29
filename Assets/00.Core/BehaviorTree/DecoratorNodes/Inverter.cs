using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 반전 노드 실패 -> 성공 / 성공 -> 실패
public class Inverter : ControlNode
{
    public Inverter(INode child) : base(child) { }

    public override NodeState Evaluate()
    {
        if(_children.Count == 0 || _children[0]==null)
        {
            _nodeState = NodeState.Failure;
            return _nodeState;
        }

        switch (_children[0].Evaluate())
        {
            case NodeState.Running:
                _nodeState = NodeState.Running;
                return _nodeState;
            case NodeState.Success:
                _nodeState = NodeState.Failure;
                return _nodeState;
            case NodeState.Failure:
                _nodeState = NodeState.Success;
                return _nodeState;
            default:
                break;
        }
        return _nodeState;
    }
}
