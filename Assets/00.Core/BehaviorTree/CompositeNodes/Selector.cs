using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 셀렉터 노드 - 자식 노드를 순서대로 실행, 하나라도 성공하면 즉시 성공을 반환(OR연산과 유사)
public class Selector : ControlNode
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
