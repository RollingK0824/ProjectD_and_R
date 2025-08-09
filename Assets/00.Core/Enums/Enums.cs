// Assets/00.Core/Enums/DamageType.cs

using System;
using Unity.Behavior;
using UnityEngine;

namespace ProjectD_and_R.Enums
{
    public enum DamageType
    {
        None = 0,
        Physical = 1,   // 물리 데미지
        Magical = 2,    // 마법 데미지
        TrueDamage = 3, // 방어력 무시 고정 데미지
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
        None,
        Deploy,
        Move,
        Attack,
        UseSkill,
        Hit,
        Idle,
        Die,
        Finished,
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


    // ----- Skill Enum ----- //
    public enum SkillEffectType // 스킬 효과 타입
    {
        None,
        Damage,             // 데미지
        Heal,               // 힐
        StatModify,         // 버프 / 디버프
        CrowdControl,       // 군중 제어기
        Shield,             // 보호막
        Summon,             // 소환
        Teleport,           // 순간이동
    }

    public enum SkillOptionType // 스킬 옵션 타입
    {
        None,

        // ----- 범위 / 전파 방식  ----- //
        Area,               // 범위 효과 여부
        ChainEffect,        // 연쇄 효과
        Piercing,           // 관통 효과
        Projectile,         // 투사체 효과
        MaxTargetCount,     // 최대 타겟 수

        // ----- 지속 시간 / 조건  ----- //
        DamageOverTime,     // 지속 피해
        DelayBeforeApply,   // 적용 전 지연 시간
        ChanceToApply,      // 적용 확률

        // ----- 기타 ----- //
        Knockback,          // 넉백 효과 
        Homing,             // 추적 효과
    }

    public enum CrowdControlType    // 군중 제어 타입
    {
        None,
        Stun,               // 기절 행동 불가
        Silence,            // 침묵 스킬 사용 불가
        Root,               // 속박 이동 불가
        Slow,               // 이동 속도 감소
    }

    public enum SkillTargetStatType // 스킬 타겟 스탯 타입
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

    public enum SkillTargetingType  // 스킬 타겟팅 타입
    {
        None,
        Self,
        SingleTarget,
        AreaOfEffect,
        AllEnemies,
        AllAllies,
        Random,
    }

    public enum SkillAreaShape
    {
        None,
        Circle,
        Cone,
        Rectangle,
        Line,
        Sector,
        Ring,
    }
}