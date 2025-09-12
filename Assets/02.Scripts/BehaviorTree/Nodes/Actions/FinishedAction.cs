using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Finished", story: "[Agent] Action Finished", category: "Action", id: "651ecc1618fda991aa88adad16c29350")]
public partial class FinishedAction : Action
{
    [SerializeReference] public BlackboardVariable<CharacterCore> Agent;

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
        if (_enemyAiComponent == null || Agent == null) return Status.Failure;

        _enemyAiComponent.ActionRequest(new FinishedActionRequset());
        return Status.Success;
    }


    private Status Initialize()
    {
        ICharacterCore character = Agent.Value as ICharacterCore;

        _enemyAiComponent = character.EnemyAiComponent;

        return Status.Running;
    }
}

