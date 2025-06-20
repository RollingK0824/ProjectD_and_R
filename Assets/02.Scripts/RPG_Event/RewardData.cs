using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RewardData
{
    [Header("골드")] public int Gold;
    [Header("아이템")] public List<ItemData> itemRewards;
}