using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequestAttackAction", story: "Request Attack Action [self]", category: "Action", id: "66731889d38ef1f5c1d162689376a638")]
public partial class RequestAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterCore> Self;
    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Self == null) return Status.Failure;

        ICharacterCore character = Self.Value as ICharacterCore;

        if(character == null) return Status.Failure;

        _enemyAiComponent = character.EnemyAiComponent;

        if(_enemyAiComponent == null) return Status.Failure;

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_enemyAiComponent == null || Self == null) return Status.Failure;

        _enemyAiComponent.ActionRequest(new AttackActionRequest());
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

