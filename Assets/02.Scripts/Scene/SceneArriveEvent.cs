using System.Diagnostics;
using UnityEngine;

public class SceneArriveEvent
{
    public void SceneEvent(string sceneName)
    {
        if (sceneName == "DungeonBattle")
        {
            RpgManager.Instance.RoomEnterSystem.battleEnter.SetBattle(EnemyType.Spider);
        }
        else if (sceneName == "RandomMapGenerator")
        {        
            GameObject parent = GameObject.Find(RpgManager.Instance.MapParent).gameObject;
            ZoneLayoutSystem zoneLayoutSystem = RpgManager.Instance.zoneLayoutSystem;

            if (RpgManager.Instance.isCreateMap)
            {
                zoneLayoutSystem.LoadMapFromBlueprint(RpgManager.Instance.mapBlueprint, RpgManager.Instance.roomPrefab, parent);
            }
            else
            {
                MapBlueprint newBlueprint = zoneLayoutSystem.GenerateNewMap(RpgManager.Instance.roomPrefab, parent);
                RpgManager.Instance.mapBlueprint = newBlueprint; 
            }
        }
    }
}
