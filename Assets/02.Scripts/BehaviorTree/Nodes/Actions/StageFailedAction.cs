using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stage Failed", story: "Stage Failed", category: "Action", id: "95cbe1cf7d8ea3872a100b9093b012a2")]
public partial class StageFailedAction : Action
{

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        StageManager.Instance.StageFailed();

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

