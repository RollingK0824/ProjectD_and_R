using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDecorator : Node
{
    private INode _conditionNode; // 조건 검사 노드
    private INode _childNode; // 조건이 충족 시 실행할 자식 노드
    public ConditionDecorator(INode conditionNode, INode childNode)
    {
        _conditionNode = conditionNode;
        _childNode = childNode;
    }
    public override NodeState Evaluate()
    {
        // 조건 노드 평가
        NodeState conditionState = _conditionNode.Evaluate();

        if (conditionState == NodeState.Success)
        {
            // 조건 성공 시 자식 노드 실행하고 결과 반환
            _nodeState = _childNode.Evaluate();
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
