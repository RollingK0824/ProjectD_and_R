using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "ScriptableObjects/Items/Equipment")]
public class EquipItem : ItemData
{
    [Header("장비 스탯")]
    public int Attack;
    public int Defense;
    public float AttackSpeed;
    public int Health;
    public float Speed;
}
