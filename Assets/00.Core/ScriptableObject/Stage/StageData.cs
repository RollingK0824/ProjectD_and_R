using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageData", menuName = "ScriptableObjects/Stage")]
public class StageData : ScriptableObject
{
    public string stageName = "New Stage";
    public float stageStartTimeOffset = 0f;

    public List<Vector2Int> enemySpawnPoints;
    public List<Vector2Int> enemyEndPoints;

    [Serializable]
    public class EnemySpawnEntry
    {
        public string enemyType; // 추후 Addreseble 타입 혹은 Dictionary에 Enemy Key값으로 활용 예정
        public int count;
        public float spawnDelay;
        public Vector2Int spawnPoint;
    }

    // --스테이지 종료 조건--
    public float stageDuration = 0f;
    public int enemyKillCount = 0;

    public List<EnemySpawnEntry> spawnSequence;
}
