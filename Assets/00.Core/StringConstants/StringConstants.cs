using UnityEngine;

namespace ProjectD_and_R.Constants
{
    public class StringConstants : MonoBehaviour
    {
        // ----- Scene Name ----- //
        public static readonly string TitleScene = "00.Title";
        public static readonly string LoadingScene = "01.Loding";
        public static readonly string DefenseScene = "02.DefenseTurn";
        public static readonly string VillageScene = "03.Village";
        public static readonly string DungeonScene = "04.Dungeon";
        public static readonly string DungeonBattleScene = "05.DungeonBattle";

        // ----- Enemy BlackBoard Variable ----- //
        public static readonly string BB_Target = "Target";
        public static readonly string BB_TurnState = "TurnState";
        public static readonly string BB_IsDeployed = "IsDeployed";
        public static readonly string BB_IsAlive = "IsAlive";
        public static readonly string BB_EndPoint = "EndPoint";

        // ----- StageManager BlackBoard Variable ----- //
        public static readonly string BB_DestroyedDefenseObjectCount = "DestroyedDefenseObjectCount";
        public static readonly string BB_MaxDefenseObjectCount = "MaxDefenseObjectCount";
        public static readonly string BB_EnemyKillCount = "EnemyKillCount";
        public static readonly string BB_MaxEnemyCount = "MaxEnemyCount";
        public static readonly string BB_PlayerDeathCount = "PlayerDeathCount";
        public static readonly string BB_MaxPlayerCount = "MaxPlayerCount";
        public static readonly string BB_IsAllEnemiesDefeated = "IsAllEnemiesDefeated";
        public static readonly string BB_IsTimeLimitReached_Clear = "IsTimeLimitReached_Clear";
        public static readonly string BB_IsBossDefeated = "IsBossDefeated";
        public static readonly string BB_IsAllPlayerUnitsDefeated = "IsAllPlayerUnitsDefeated";
        public static readonly string BB_IsDefenseTargetDestroyed = "IsDefenseTargetDestroyed";
        public static readonly string BB_IsTimeLimitReached_GameOver = "IsTimeLimitReached_GameOver";

        // ----- Unit Address ----- //
        public static readonly string DefaultUnitAddress = "Assets/03.Prefabs/Characters/DefaultUnit.prefab";
        public static readonly string DefaultEnemyUnitAddress = "Assets/03.Prefabs/Characters/DefaultEnemyUnit.prefab";

        // ----- Behehavior Graph Address ----- //
        public static readonly string MainEnemyBehaviorGraphAddress = "Assets/09.GameDatas/BehaviorTree/EnemyAi/BT_EnemyMain.asset";
    }
}


