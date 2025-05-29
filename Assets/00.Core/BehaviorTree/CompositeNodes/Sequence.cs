using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시퀀스 노드 - 자식 노드를 순서대로 실행, 모든 노드가 성공해야 성공을 변환(AND연산과 유사)
public class Sequence : ControlNode
{
    public Sequence() : base() { }
    public Sequence(List<INode> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool anyChildrenIsRunning = false;
        foreach(var child in _children)
        {
            switch (child.Evaluate())
            {
                case NodeState.Running:
                    anyChildrenIsRunning = true;
                    break;
                case NodeState.Success:
                    continue;   // 다음 자식 노드 실행
                case NodeState.Failure:
                    _nodeState = NodeState.Failure;
                    return _nodeState;
                default:
#if UNITY_EDITOR
                    Debug.Log($"잘못된 노드 입력");
#endif
                    break;
            }
        }

        _nodeState = anyChildrenIsRunning ? NodeState.Running : NodeState.Success;
        return _nodeState;
    }
}
