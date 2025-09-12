using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class TurnBattleManager : Singleton<TurnBattleManager>
{
    private List<ICharacterCore> _allCharacters;
    private Queue<ICharacterCore> _turnQueue;

    private bool _battleEnded = false;

    public void StartNewRound()
    {
        _allCharacters = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<ICharacterCore>()
            .Where(u => u.CharacterStatus.IsDeployed && u.CharacterStatus.IsAlive)
            .ToList();
        
        StartCoroutine(BattleLoop());
    }

    private IEnumerator BattleLoop()
    {
        while(!_battleEnded)
        {
            if(_turnQueue.Count == 0)
            {
                PrepareNewRound();
            }

            var currentUnit = _turnQueue.Dequeue();

            if (!currentUnit.CharacterStatus.IsAlive)
            {
                continue;
            }

#if UNITY_EDITOR
            Debug.Log($"[{this.name}]/{currentUnit.Data.name}의 턴 시작");
#endif
            yield return currentUnit.TurnComponent.StartTurn();
        }

#if UNITY_EDITOR
        Debug.Log($"[{this.name}]/전투 종료");
#endif
    }

    private void PrepareNewRound()
    {
        var aliveUnits = _allCharacters
            .Where(u => u.CharacterStatus.IsAlive)
            .OrderByDescending(u => u.CharacterStatus.AttackSpeed)
            .ToList();

        _turnQueue = new Queue<ICharacterCore>(aliveUnits);
    }

    public void EndBattle()
    {
        _battleEnded = true;
    }


}
