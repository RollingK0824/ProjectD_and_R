using NUnit.Framework;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class BattleEnter
{
    [SerializeField][Header("현재 적 종류")] public EnemyType EnemyType;

    /// <summary>
    /// 배틀 세팅
    /// </summary>
    public void SetBattle(EnemyType race)
    {
        List<RoomEnemySpawnData> StageData = RpgManager.Instance.Database.StageData;

        int rand = Random.Range(0, StageData.Count);
        List<EnemyCharacterData> enemys = RpgManager.Instance.UnitSystem.GetRaceToUnits(race);
        for (int i = 0; i < StageData[rand].spawnPositions.Count; i++)
        {
            SpawnUnit(StageData[rand].spawnPositions[i], enemys[Random.Range(0, enemys.Count)].CharacterPrefab);
        }
    }

    /// <summary>
    /// 몬스터 생성
    /// </summary>

    //몬스터 컨테이너 만들어서 가져오는걸로하자
    void SpawnUnit(Vector2 SpawnPoint, GameObject prefab)
    {
        GameObject temp = SearchUnit(prefab);
        temp.SetActive(true);
        Vector3 pos = GridManager.Instance.GridToWorldPos(SpawnPoint);
        temp.transform.position = new Vector3(pos.x, 1, pos.z);
    }

    GameObject SearchUnit(GameObject prefab)
    {
        List<GameObject> units = new List<GameObject>();
        GameObject container = GameObject.FindWithTag("UnitContainer");
        for (int i = 0; i<container.transform.childCount;i++ )
        {
            units.Add(container.transform.GetChild(i).gameObject);
        }

        foreach (GameObject unit in units) 
        {
            if (unit.name == prefab.name && !unit.activeSelf)
                return unit;
        }
        return null;
    }

    public void endBattle()
    {
        GameObject container = GameObject.FindWithTag("UnitContainer");
        for (int i = 0; i < container.transform.childCount; i++)
        {
            container.transform.GetChild(i).gameObject.SetActive(false);
        }

        GameManager.Instance.GoToScene("RandomMapGenerator");
    }
}
