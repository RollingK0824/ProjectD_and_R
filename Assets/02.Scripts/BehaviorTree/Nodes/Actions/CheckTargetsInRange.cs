using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTargetsInRange", story: "Checks if the current target is within attack range [self] [Agent] [AttackRange]", category: "Action", id: "087d9bdd6efa559c2502759a686da4df")]
public partial class CheckTargetsInRange : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<CharacterCore> Agent;
    [SerializeReference] public BlackboardVariable<float> AttackRange;

    private IEnemyAi _enemyAiComponent;

    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        Vector2Int currentGridPos = GridManager.Instance.WorldToGridPos(Self.Value.transform.position);
        List<IGridObject> targets = GridManager.Instance.GetObjectsInRadius(currentGridPos, AttackRange);

        IGridObject closestTarget = null;
        float minDistance = float.MaxValue;
        foreach (var target in targets)
        {
            if (target.GameObject == Self.Value) continue;

            if (target.ObjectType == ProjectD_and_R.Enums.ObjectType.Player || target.ObjectType == ProjectD_and_R.Enums.ObjectType.Obstacle)
            {
                float distance = Vector2.Distance(currentGridPos, target.CurrentGridPos);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = target;
                }
            }
        }

        if (closestTarget != null)
        {
            _enemyAiComponent.StatusChanged<GameObject>("Target",null,closestTarget.GameObject);
            return Status.Success;
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

