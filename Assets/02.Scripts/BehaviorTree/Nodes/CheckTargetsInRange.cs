using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTargetsInRange", story: "Checks if the current target is within attack range [self] [targets] [AttackRange]", category: "Action", id: "087d9bdd6efa559c2502759a686da4df")]
public partial class CheckTargetsInRange : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<CharacterCore> Agent;
    [SerializeReference] public BlackboardVariable<List<Vector3>> Targets;
    [SerializeReference] public BlackboardVariable<float> AttackRange;

    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Agent.Value == null || Targets.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        foreach (var target in Targets.Value)
        {
            float sqrDist = (Self.Value.transform.position - target).sqrMagnitude;
            if (sqrDist <= AttackRange * AttackRange)
            {
#if UNITY_EDITOR
                Debug.Log($"타겟이 공격 범위 내이 있음");
#endif
                _enemyAiComponent.StatusChanged<bool>("IsTargetInRange", false, true);
                return Status.Success;
            }
        }

        return Status.Failure;
    }

    private Status Initialize()
    {
        ICharacterCore character = Agent.Value as ICharacterCore;

        _enemyAiComponent = character.EnemyAiComponent;

        return Status.Running;
    }
}

