using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequestMoveAction", story: "Request Move To [Target] Action [self]", category: "Action", id: "a6e9ec38f5fb6b98b7d793d86e68d47e")]
public partial class RequestMoveAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<CharacterCore> Self;
    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Self == null) return Status.Failure;

        ICharacterCore character = Self.Value as ICharacterCore;

        _enemyAiComponent = character.EnemyAiComponent;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_enemyAiComponent == null || Self == null) return Status.Failure;

        _enemyAiComponent.ActionRequest(new MoveActionRequest(Target.Value.position));

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

