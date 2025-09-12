using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomEnemySpawnData", menuName = "ScriptableObjects/RoomEnemySpawnDatas")]

public class RoomEnemySpawnData : ScriptableObject
{
    [Header("몬스터 배치 그리드 좌표")] public List<Vector2Int> spawnPositions = new List<Vector2Int>();
}
