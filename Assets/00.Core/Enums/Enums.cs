// Assets/00.Core/Enums/DamageType.cs

using System;
using Unity.Behavior;

namespace ProjectD_and_R.Enums
{
    public enum DamageType
    {
        Physical = 0,   // 물리 데미지
        Magical = 1,    // 마법 데미지
        TrueDamage = 2, // 방어력 무시 고정 데미지
    }

    [Flags]
    public enum MoveType
    {
        None = 0,   // 어떤 지형도 이동 불가
        Ground = 1 << 0,    // 지상 이동 가능
        Water = 1 << 1, // 물 이동 가능
        Air = 1 << 2,   // 공중 이동 가능
        Wall = 1 << 3,  // 벽 이동 가능
        All = Ground | Water | Air | Wall   // 모든 지형 이동 가능
    }

    public enum Faction
    {
        None = 0,
        Player = 1,
        Enemy = 2,
    }
    public enum ObjectType
    {
        None,
        Player,
        Enemy,
        Boss,
        Obstacle,
        DefenseTarget,
    }

    public enum GridCellType
    {
        None = 0,
        Path = 1,
        Buildable = 2,
        Obastacle = 3,
        SpawnPoint = 4,
        EndPoint = 5,
    }

    public enum ActionType
    {
        Deploy,
        Move,
        Attack,
        UseSkill,
        Hit,
        Idle,
        Die,
        Finished,
    }

    public enum SkillEffectType
    {
        None,
        PhysicalDamage,
        MagicalDamage,
        TrueDamage,
        Heal,
        Buff,
        Debuff,
        Stun,
        Shield,
        Summon,
        Teleport,
    }

    public enum SkillTargetStatType
    {
        None,
        MaxHealth,
        CurrentHealth,
        PhysicalDefense,
        MagicalResistance,
        AttackDamage,
        AttackSpeed,
        AttackRange,
        MoveSpeed,
    }

    public enum SkillTargetingType
    {
        None,
        Self,
        SingleTarget,
        AreaOfEffect,
        RandomTarget,
        Line,
        Circle,
        Cone,
    }

    public enum GameEndConditionType
    {
        // 클리어 조건
        AllEnemiesDefeated,
        TimeLimitReached_Clear,
        BossDefeated,

        // 게임 오버 조건
        AllPlayerUnitsDefeated,
        DefenseTargetDestroyed,
        TimeLimitReached_GameOver,
    }

    public enum GameState
    {
        MainMenu,
        Loading,
        SceneStarting,
        SceneInProgress,
        ScenePaused,
        StageEnded,
        GameOver
    }

    [BlackboardEnum]
    public enum TurnState
    {
        None,
        DefenseTurn,
        DungeonTurn,
        DungeonBattleTurn,
        VillageTurn,
    }

    public enum CurrentScene
    {
        None,
        Title,
        Loading,
        DefenseScene,
        DungeonScene,
        DungeonBattleScene,
    }

    public enum CharacterRarity
    {
        None,
        Common,
        UnCommon,
        Unique,
    }

}