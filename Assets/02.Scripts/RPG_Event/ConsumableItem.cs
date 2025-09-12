using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "ScriptableObjects/Items/Consumable")]
public class ConsumableData : ItemData, Iuseble
{
    [Header("소모품 효과")]
    public float HealAmount; // 체력 회복량

    public void Use()
    {
        
    }
}

