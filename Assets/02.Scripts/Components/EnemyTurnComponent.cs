using System.Collections;
using UnityEngine;

public class EnemyTurnComponent : TurnComponent
{
    public override IEnumerator StartTurn()
    {
        _isActionFinished = false;

        _characterCore.BehaviorGraphAgent?.Start();
        yield return new WaitUntil(() => _isActionFinished);

        EndTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();
    }
}
