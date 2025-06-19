using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequestAttackAction", story: "Request Attack Action [self] [Target]", category: "Action", id: "66731889d38ef1f5c1d162689376a638")]
public partial class RequestAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterCore> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Self.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        if (_enemyAiComponent == null || Self.Value == null) return Status.Failure;

        _enemyAiComponent.ActionRequest(new AttackActionRequest(Target));
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private Status Initialize()
    {
        ICharacterCore character = Self.Value as ICharacterCore;

        _enemyAiComponent = character.EnemyAiComponent;

        return Status.Running;
    }
}

