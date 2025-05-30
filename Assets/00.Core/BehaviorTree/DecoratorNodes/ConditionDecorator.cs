using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDecorator : ControlNode
{
    private INode _conditionNode; // 조건 검사 노드
    public ConditionDecorator(INode conditionNode, INode childNode) : base(childNode)
    {
        _conditionNode = conditionNode;
    }
    public override NodeState Evaluate()
    {
        if (_children.Count == 0 || _children[0] == null)
        {
            _nodeState = NodeState.Failure;
            return _nodeState;
        }

        // 조건 노드 평가
        NodeState conditionState = _conditionNode.Evaluate();

        if (conditionState == NodeState.Success)
        {
            // 조건 성공 시 자식 노드 실행하고 결과 반환
            _nodeState = _children[0].Evaluate();
            return _nodeState;
        }  
        else if (conditionState == NodeState.Failure)
        {
            // 조건 실패 시 자식 노드 실행하지 않고 Failure반환
            _nodeState = NodeState.Failure;
            return _nodeState;
        }
        else
        {
            _nodeState = NodeState.Running;
            return _nodeState;
        }
    }

    
}
