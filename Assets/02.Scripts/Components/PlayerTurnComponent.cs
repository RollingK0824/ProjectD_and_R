using System.Collections;
using UnityEngine;

public class PlayerTurnComponent : TurnComponent
{
    public override IEnumerator StartTurn()
    {
        _isActionFinished = false;

        TurnBattleUI.Instance.SetActiveUnit(_characterCore);

        yield return new WaitUntil(() => _isActionFinished);

        EndTurn();
    }

    public override void EndTurn()
    {
        base.EndTurn();
        TurnBattleUI.Instance.UnActiveUnit();
    }
}
