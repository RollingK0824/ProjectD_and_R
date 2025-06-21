using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RequestMoveStopAction", story: "Request Move Stop Action [Agent]", category: "Action", id: "19b955a8103face662bf19860f0f02e9")]
public partial class RequestMoveStopAction : Action
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

        _enemyAiComponent.ActionRequest(new MoveStopActionRequest());
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    private Status Initialize()
    {
        ICharacterCore character = Agent.Value as ICharacterCore;

        _enemyAiComponent = character.EnemyAiComponent;

        return Status.Running;
    }
}

