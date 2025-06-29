using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RpgDatabase 
{
    [SerializeField][Header("모든 이벤트 정보")] public List<EventData> events;
    [SerializeField][Header("모든 아이템 정보")] public List<ItemData> Items;
    [SerializeField][Header("모든 스테이지 정보")] public List<RoomEnemySpawnData> StageData;
    [SerializeField][Header("모든 유닛 정보")] public List<EnemyCharacterData> Units;
}
