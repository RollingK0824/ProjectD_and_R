using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 반전 노드 실패 -> 성공 / 성공 -> 실패
public class Inverter : Node
{
    private INode _child;

    public Inverter(INode child)
    {
        _child = child;
    }

    public override NodeState Evaluate()
    {
        switch (_child.Evaluate())
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
