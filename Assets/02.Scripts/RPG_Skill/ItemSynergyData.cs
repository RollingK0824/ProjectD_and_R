using UnityEngine;
using System.Collections.Generic;

public struct StatMultipliers
{
    public float Attack;   
    public float Defense;
    public float AttackSpeed;
    public float Health;
    public float Speed;
}

[CreateAssetMenu(fileName = "New Synergy", menuName = "ScriptableObjects/Synergies")]
public class ItemSynergyData : ScriptableObject
{
    [Header("시너지 이름")]
    public string synergyName;

    [Tooltip("이 시너지를 구성하는 세트 아이템 목록")]
    public List<EquipItem> requiredItems;

    [Tooltip("시너지를 발동시키기 위해 필요한 세트 아이템의 최소 개수")]
    public int requiredCount;

    [Header("활성화 시 추가될 스탯")]
    public StatBoosts synergyStats; 

    [Header("활성화 시 추가될 특수 능력")]
    public IAbility synergyAbility;

    [Header("비율(%) 스탯 보너스")]
    public StatMultipliers synergyMultipliers;
}