using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "RequestMoveAction", 
    story: "Request Move To [Target] Action [Agent]", 
    category: "Action", 
    id: "a6e9ec38f5fb6b98b7d793d86e68d47e")]
public partial class RequestMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> Target;
    [SerializeReference] public BlackboardVariable<CharacterCore> Agent;

    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Target.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        if (_enemyAiComponent == null || Agent == null) return Status.Failure;

        _enemyAiComponent.ActionRequest(new MoveActionRequest(Target.Value));

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private Status Initialize()
    {
        ICharacterCore character = Agent.Value as ICharacterCore;

        _enemyAiComponent = character.EnemyAiComponent;

        return Status.Running;
    }
}

