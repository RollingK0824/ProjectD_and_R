using System.Collections;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public interface ICharacterCore
{
    CharacterData Data { get; }
    ICharacterStatus CharacterStatus { get; }
    IDamageable DamageableComponent { get; }
    IMovable MovementComponent { get; }
    NavMeshAgent NavMeshAgent { get; }
    IAttacker AttackerComponent { get; }
    IDeployable DeployableComponent { get; }
    IEnemyAi EnemyAiComponent { get; }
    BehaviorGraphAgent BehaviorGraphAgent { get; }
    IGridObject GridObject { get; }
    ISkillComponent SkillComponent { get; }
    ProjectD_and_R.Enums.TurnState TurnState { get; }
    ITurnComponent TurnComponent { get; }
    GameObject GameObject { get; }
}
