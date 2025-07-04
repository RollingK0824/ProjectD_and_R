using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("아이디")]public int Item_ID;       
    [Header("이름")] public string Item_Name;
    [Header("타입")] public ItemType Type;
    [Header("등급")] public ItemGrade Grade;
    [Header("가격")] public int Price;

    [TextArea][Header("설명")] public string Item_Description; 
}
