using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;
using System.Linq;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DefenseTargetDestroyedCondition", story: "Fail : DefenseObject Destroyed [Current] / [Required]", category: "Action", id: "595dba83a4122b42f32c5d5f2a559c3c")]
public partial class DefenseTargetDestroyedConditionAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Current;
    [SerializeReference] public BlackboardVariable<int> Required;

    protected override Status OnStart()
    {
        

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private Status Initialize()
    {
        

        return Status.Running;
    }
}

