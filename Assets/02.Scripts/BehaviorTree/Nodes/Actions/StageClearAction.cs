using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StageClear", story: "Stage Clear", category: "Action", id: "9e33e805dc100564b7fce702d24ef7c8")]
public partial class StageClearAction : Action
{
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        StageManager.Instance.StageClear();

        return Status.Success;
    }
}

