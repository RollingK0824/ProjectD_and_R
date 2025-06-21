using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/Items")]
public class ItemData : ScriptableObject
{
    [Header("아이디")]public int Item_ID;       
    [Header("이름")] public string Item_Name;
    [Header("타입")] public ItemType Type;
    [Header("등급")] public ItemGrade Grade;
    [Header("가격")] public int Price;

    [TextArea][Header("설명")] public string Item_Description;

    [Header("공격력")] public int Attack;          
    [Header("방어력")] public int Defense;         
    [Header("공격속도")] public float AttackSpeed;  
    [Header("체력")] public int Health;
    [Header("이동속도")] public float Speed;
 
}
