using System.Collections;
using Unity.Behavior;
using UnityEngine;

public class EnemyTurnComponent : TurnComponent
{
    private void Start()
    {
        _characterCore.BehaviorGraphAgent.enabled = false;
    }

    public override IEnumerator StartTurn()
    {
        _isActionFinished = false;

        _characterCore.BehaviorGraphAgent.enabled = true;

        _characterCore.BehaviorGraphAgent?.Restart();
        yield return new WaitUntil(() => _isActionFinished);

        EndTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();

        _isActionFinished = false;
        _characterCore.BehaviorGraphAgent.enabled = false;
    }
}
