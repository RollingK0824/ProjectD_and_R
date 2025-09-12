using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SuccessNode", story: "SuccessNode", category: "Action", id: "c5a088db8d88a8818926930fadb06eda")]
public partial class SuccessNode : Action
{
    protected override Status OnUpdate()
    {
        return Status.Success;
    }
}

