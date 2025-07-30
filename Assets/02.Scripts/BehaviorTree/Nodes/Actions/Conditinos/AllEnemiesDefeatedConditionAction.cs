using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.TextCore.Text;
using System.Runtime.Remoting.Messaging;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AllEnemiesDefeatedCondition", story: "Clear: Enemy Kills [Current] / [Required]", category: "Action", id: "a926b2c9cd0a720e9c1b6c12d2f717e2")]
public partial class AllEnemiesDefeatedConditionAction : Action
{
    [SerializeReference] public BlackboardVariable<int> Current;
    [SerializeReference] public BlackboardVariable<int> Required;
    protected override Status OnStart()
    {
        if (Current == null || Required == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        if(Current.Value >= Required.Value)
        {
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }

    }

    protected override void OnEnd()
    {
    }

    private Status Initialize()
    {
        

        return Status.Running;
    }
}

