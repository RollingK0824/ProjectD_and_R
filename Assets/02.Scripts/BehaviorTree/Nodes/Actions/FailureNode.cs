using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FailureNode", story: "FailureNode", category: "Action", id: "bd7c71761a6ebc79790a429ca4501273")]
public partial class FailureNode : Action
{
    protected override Status OnUpdate()
    {
        return Status.Failure;
    }
}

