using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequestMoveStopAction", story: "Request Move Stop Action [self]", category: "Action", id: "19b955a8103face662bf19860f0f02e9")]
public partial class RequestMoveStopAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterCore> Self;
    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Self == null) return Status.Failure;

        ICharacterCore character = Self.Value as ICharacterCore;

        if (character == null) return Status.Failure;

        _enemyAiComponent = character.EnemyAiComponent;

        if (_enemyAiComponent == null) return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_enemyAiComponent == null || Self == null) return Status.Failure;

        _enemyAiComponent.ActionRequest(new MoveStopActionRequest());
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

