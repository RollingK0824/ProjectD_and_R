using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskNode : Node
{
    protected Blackboard _blackBoard;

    public TaskNode(Blackboard blackboard)
    {
        _blackBoard = blackboard;
    }

    public override abstract NodeState Evaluate();
}
