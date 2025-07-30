using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MercenaryGuild : MonoBehaviour
{
    public int hireCost = 500; 
    public int numberOfSlots = 3; 

    private List<CharacterData> _unitsForHire = new List<CharacterData>();
    public List<CharacterData> UnitsForHire => _unitsForHire;

    /// <summary>
    /// 고용 가능한 용병 목록 갱신
    /// </summary>
    public void RefreshHirableUnits()
    {
        _unitsForHire.Clear();

        List<CharacterData> lockedUnits = RpgManager.Instance.Database.Units
            .Where(u => !PartyManager.Instance.IsUnitUnlocked(u.id))
            .ToList();

        if (lockedUnits.Count == 0)
            return;

        for (int i = 0; i < numberOfSlots && lockedUnits.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, lockedUnits.Count);
            _unitsForHire.Add(lockedUnits[randomIndex]);
            lockedUnits.RemoveAt(randomIndex);
        }

    }

    /// <summary>
    /// 특정 유닛 고용 시도
    /// </summary>
    public bool HireUnit(CharacterData unitToHire)
    {
        if (unitToHire == null || !_unitsForHire.Contains(unitToHire)) return false;

         if (RpgManager.Instance.inventory.Gold < hireCost)
             return false;
         
        RpgManager.Instance.inventory.SpendGold(hireCost);

        PartyManager.Instance.UnlockUnit(unitToHire.id);

        _unitsForHire.Remove(unitToHire);
        return true;
    }
}