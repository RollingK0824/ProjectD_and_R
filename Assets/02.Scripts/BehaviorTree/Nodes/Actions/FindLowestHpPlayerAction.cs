using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindLowestHPPlayer", story: "Lowest HP Player Find [Targets] [Target]", category: "Action", id: "83f71f84758dbf2dd834fba063537187")]
public partial class FindLowestHpPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    float minValue = float.MaxValue;

    private List<ICharacterCore> _characterCores;

    protected override Status OnStart()
    {
        if (Targets.Value == null)
        {
            return Status.Failure;
        }

        return Initialize();
    }

    protected override Status OnUpdate()
    {
        // Player List를 순회 하여 체력이 제일 적은 플레이어를 Target으로 지정
        foreach (var character in _characterCores)
        {
            if (character.CharacterStatus.CurrentHealth < minValue)
            {
                minValue = character.CharacterStatus.CurrentHealth;
                Target.Value = character.GameObject;
            }
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private Status Initialize()
    {
        _characterCores = new List<ICharacterCore>();

        foreach (GameObject gameObject in Targets.Value)
        {
            gameObject.TryGetComponent<ICharacterCore>(out var tempCharacterCore);
            _characterCores.Add(tempCharacterCore);
        }

        return Status.Running;
    }

}

