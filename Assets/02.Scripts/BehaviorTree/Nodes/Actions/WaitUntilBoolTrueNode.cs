using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitUntilBoolTrueNode", story: "[Bool] Wating", category: "Action", id: "43136e8fff1a5bccfa5b798dd4719ee0")]
public partial class WaitUntilBoolTrueNode : Action
{
    [SerializeReference] public BlackboardVariable<bool> Bool;

    protected override Status OnUpdate()
    {
        if(Bool == null)
        {
#if UNITY_EDITOR
            Debug.Log($"[WaitUntilBoolTrueNode] / Bool Is NUll");
#endif
            return Status.Failure;
        }

        return Bool.Value ? Status.Success : Status.Running;
    }
}

