using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 반복 노드 특정 횟수만큼 반복 혹은 특정 조건 충족까지 실행
public class Repeater : Node
{
    private INode _child;
    public Repeater(INode child)
    {
        _child = child;
    }

    public override NodeState Evaluate()
    {
        switch (_child.Evaluate())
        {
            case NodeState.Running:
                break;
            case NodeState.Success:
                break;
            case NodeState.Failure:
                break;
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
