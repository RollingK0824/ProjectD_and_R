using System.Diagnostics;
using UnityEngine;
using ProjectD_and_R.Constants;
using ProjectD_and_R.Enums;

public class SceneArriveEvent
{
    public void SceneEvent(string sceneName)
    {
        if (sceneName == "DungeonBattle")
        {
            RpgManager.Instance.RoomEnterSystem.battleEnter.SetBattle(UnitType.Spider);
        }
        else if (sceneName == "04.Dungeon")
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

            if (RpgManager.Instance.CheckEndOfRPG()) RpgManager.Instance.EndRPG();
        }

        if (sceneName == StringConstants.TitleScene)
        {
            GameManager.Instance.SetGameState(GameState.MainMenu);
        }
        else if (sceneName == StringConstants.LoadingScene)
        {
            GameManager.Instance.SetGameState(GameState.Loading);
        }
        else if (sceneName == StringConstants.VillageScene)
        {
            GameManager.Instance.SetGameState(GameState.SceneStarting);
            GameManager.Instance.SetTurnState(ProjectD_and_R.Enums.TurnState.VillageTurn);
        }
        else if (sceneName == StringConstants.DefenseScene)
        {
            GameManager.Instance.SetGameState(GameState.SceneStarting);
            GameManager.Instance.SetTurnState(ProjectD_and_R.Enums.TurnState.DefenseTurn);
        }
        else if (sceneName == StringConstants.DungeonScene)
        {
            GameManager.Instance.SetGameState(GameState.SceneStarting);
            GameManager.Instance.SetTurnState(ProjectD_and_R.Enums.TurnState.DungeonTurn);
        }
        else if (sceneName == StringConstants.DungeonBattleScene)
        {
            GameManager.Instance.SetGameState(GameState.SceneStarting);
            GameManager.Instance.SetTurnState(ProjectD_and_R.Enums.TurnState.DungeonBattleTurn);
        }
    }
}
