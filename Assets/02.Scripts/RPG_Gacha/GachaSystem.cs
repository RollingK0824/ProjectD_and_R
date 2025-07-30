using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GachaSystem : MonoBehaviour
{
    public int singlePullCost = 100;
    public int tenPullCost = 1000;

    /// <summary>
    /// 1회 뽑기
    /// </summary>
    public void PerformSinglePull()
    {
        if (RpgManager.Instance.inventory.Gold < singlePullCost)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }

        RpgManager.Instance.inventory.Gold-= singlePullCost;

        List<CharacterData> allUnits = RpgManager.Instance.Database.Units;
        if (allUnits == null || allUnits.Count == 0) return;

        CharacterData summonedUnit = allUnits[Random.Range(0, allUnits.Count)];

        PartyManager.Instance.UnlockUnit(summonedUnit.id);
    }

    /// <summary>
    /// 10회 뽑기 
    /// </summary>
    public void PerformTenPull()
    {
        if (RpgManager.Instance.inventory.Gold < tenPullCost)
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }

        RpgManager.Instance.inventory.Gold -= tenPullCost;

        List<CharacterData> allUnits = RpgManager.Instance.Database.Units;
        if (allUnits == null || allUnits.Count == 0) return;

        List<CharacterData> summonedUnits = new List<CharacterData>();
        bool hasGuaranteedRare = false;

        for (int i = 0; i < 9; i++)
        {
            CharacterData unit = allUnits[Random.Range(0, allUnits.Count)];
            summonedUnits.Add(unit);
            if (unit.Grade >= UnitGrade.Rare)
            {
                hasGuaranteedRare = true;
            }
        }

        if (!hasGuaranteedRare)
        {
            List<CharacterData> rareOrHigherUnits = allUnits.Where(u => u.Grade >= UnitGrade.Rare).ToList();
            if (rareOrHigherUnits.Count > 0)
            {
                summonedUnits.Add(rareOrHigherUnits[Random.Range(0, rareOrHigherUnits.Count)]);
            }
            else 
            {
                summonedUnits.Add(allUnits[Random.Range(0, allUnits.Count)]);
            }
        }
        else 
        {
            summonedUnits.Add(allUnits[Random.Range(0, allUnits.Count)]);
        }

        foreach (var unit in summonedUnits)
        {
            PartyManager.Instance.UnlockUnit(unit.id);
            Debug.Log($"- {unit.name} ({unit.Grade})");
        }
    }
}
