
using ProjectD_and_R.Enums;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObjects/Skill/SKillEffect")]
public class SkillEffect : ScriptableObject
{
    [Header("Core")]
    public SkillEffectType effectType;          // skill 효과 타입 (예: Damage, Heal 등)
    public float amount;                        // 배율 (피해량, 회복량, 스탯 변화량 등) 
    public float duration;                      // 지속 시간 (버프, 디버프, 군중 제어 효과 등)

    [Header("Damage")]
    public DamageType damageType;          // 스킬 데미지 타입 (예: Physical, Magical, True 등)
        
    [Header("Buff / Debuff")]
    public SkillTargetStatType targetStat;      // 스킬 타겟 스탯 타입 (예: Health, Mana 등)


    [Header("Crowd Control")]
    public CrowdControlType crowdControlType;   // 군중 제어 타입 (예: Stun, Slow 등)

    [Header("Area & Shape")]
    public SkillAreaShape areaShape;            // 스킬 범위 모양 (예: Circle, Rectangle 등)
    public float areaSize;                      // 스킬 범위 크기 (예: 원의 반지름, 사각형의 너비 등)
    public Vector2 areaOffset;                  // 스킬 범위 오프셋 (예: 원의 중심 위치, 사각형의 시작 위치 등)   

    [Header("Execution")]
    public float delayBeforeApply;              // 적용 전 지연 시간 (예: 0.5초 후에 효과 적용)
    public float chanceToApply;                 // 적용 확률 (예: 0.2 = 20% 확률로 효과 적용)
    public bool applyOncePerTarget;             // 타겟당 한 번만 적용 여부

    public List<SkillOption> options;           // 부과 효과 옵션
}