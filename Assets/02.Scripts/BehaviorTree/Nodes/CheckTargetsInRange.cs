using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTargetsInRange", story: "Checks if the current target is within attack range [self] [targets] [AttackRange]", category: "Action", id: "087d9bdd6efa559c2502759a686da4df")]
public partial class CheckTargetsInRange : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<List<Vector3>> Targets;
    [SerializeReference] public BlackboardVariable<float> AttackRange;

    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        GameObject self = Self.Value;
        _enemyAiComponent = self.GetComponent<IEnemyAi>();

        if (self == null || Targets.Value.Count < 0) return Status.Success;

        foreach (var target in Targets.Value)
        {
            float sqrDist = (self.transform.position - target).sqrMagnitude;
            if (sqrDist <= AttackRange * AttackRange)
            {
#if UNITY_EDITOR
                Debug.Log($"타겟이 공격 범위 내이 있음");
#endif
                _enemyAiComponent.OnStatusChanged<bool>("IsTargetInRange", false, true);
            }
        }

        return Status.Success;
    }
}

