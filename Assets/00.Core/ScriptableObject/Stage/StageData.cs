using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStageData", menuName = "ScriptableObjects/Stage/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName = "New Stage";
    public float stageStartTimeOffset = 0f;
    public Vector2Int endPoint;

    [Serializable]
    public class EnemySpawnEntry
    {
        public string enemyType; // 추후 Addreseble 타입 혹은 Dictionary에 Enemy Key값으로 활용 예정
        public int count;
        public float spawnDelay;
        public Vector2Int spawnPoint;
        public Vector2Int endPoint;
    }

    public List<EnemySpawnEntry> spawnSequence;

    // --스테이지 종료 조건--
    public StageConditionData stageConditionData;
}
