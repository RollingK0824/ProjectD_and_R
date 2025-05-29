using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionNode : Node
{
    protected Blackboard _blackboard;

    public ConditionNode(Blackboard blackboard) : base()
    {
        _blackboard = blackboard;
    }

    public abstract override NodeState Evaluate();
}
