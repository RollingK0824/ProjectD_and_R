using Unity.AppUI.UI;
using UnityEngine;
using System.Collections.Generic;

public class TurnBattleUI : Singleton<TurnBattleUI> 
{
    public TMPro.TMP_Text unitInfoTMP;

    [SerializeField] private CharacterCore _debugUnit;
    private ICharacterCore _currentUnit;

    public void SetActiveUnit(ICharacterCore character)
    {
        _currentUnit = character;
        _debugUnit = _currentUnit as CharacterCore;

        unitInfoTMP.text = _currentUnit.Data.name;
    }

    public void UnActiveUnit()
    {
        _currentUnit = null;
        _debugUnit = null;
        unitInfoTMP.text = "";
    }

    public void OnAttackBtnClick()
    {
        _currentUnit.AttackerComponent.TryAttack();
#if UNITY_EDITOR
        Debug.Log($"[{this.name}] / {_currentUnit.Data.name}공격 실행");
        // 임시 로직
        _currentUnit.TurnComponent.NotifyActionFinished();
#endif
    }

    
}
