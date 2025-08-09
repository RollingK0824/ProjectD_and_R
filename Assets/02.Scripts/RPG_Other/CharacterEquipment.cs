using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public struct StatBoosts
{
    public int Attack;
    public int Defense;
    public float AttackSpeed;
    public int Health;
    public float Speed;
}

public class CharacterEquipment : MonoBehaviour
{
    [Header("시너지 정보")]
    [SerializeField] private List<ItemSynergyData> _availableSynergies;

    [Header("장착 아이템")]
    EquipItem[] _equipmentSlots = new EquipItem[3];

    //활성화된 시너지들
    private List<ItemSynergyData> _activeSynergies = new List<ItemSynergyData>();

    /// <summary>
    /// 현재 장착 중인 모든 장비의 스탯 합계 반환
    /// </summary>
    public StatBoosts GetTotalEquippedStats()
    {
        StatBoosts totalStats = new StatBoosts();

        foreach (EquipItem item in _equipmentSlots.Where(slot => slot != null))
        {
            totalStats.Attack += item.Attack;
            totalStats.Defense += item.Defense;
            totalStats.AttackSpeed += item.AttackSpeed;
            totalStats.Health += item.Health;
            totalStats.Speed += item.Speed;
        }

        return totalStats;
    }

    /// <summary>
    /// 특정 슬롯에 아이템을 장착
    /// </summary>
    public EquipItem Equip(int slotIndex, EquipItem itemToEquip)
    {
        if (slotIndex < 0 || slotIndex >= _equipmentSlots.Length)
        {
            return itemToEquip; 
        }

        EquipItem oldItem = _equipmentSlots[slotIndex];
        _equipmentSlots[slotIndex] = itemToEquip;

        RecalculateSynergies();

        return oldItem;
    }

    /// <summary>
    /// 특정 슬롯의 아이템을 장착 해제
    /// </summary>
    public EquipItem UnEquip(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= _equipmentSlots.Length || _equipmentSlots[slotIndex] == null)
        {
            return null;
        }

        EquipItem unequippedItem = _equipmentSlots[slotIndex];
        _equipmentSlots[slotIndex] = null;

        RecalculateSynergies();

        return unequippedItem;
    }

    /// <summary>
    /// 현재 장비들을 기반으로 활성화된 시너지를 다시 계산
    /// </summary>
    private void RecalculateSynergies()
    {
        _activeSynergies.Clear();

        foreach (var synergy in _availableSynergies)
        {
            if (synergy == null) continue;

            int count = _equipmentSlots.Count(equippedItem => equippedItem != null && synergy.requiredItems.Contains(equippedItem));

            if (count >= synergy.requiredCount)
            {
                _activeSynergies.Add(synergy);
            }
        }
    }

}
