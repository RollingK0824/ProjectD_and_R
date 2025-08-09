using UnityEngine;
using ProjectD_and_R.Enums;
using System.Collections.Generic;
using NUnit.Framework;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObjects/Skill/SKillData")]
public class SkillData : ScriptableObject
{
    [Header("Info")]
    public string skillId; // 스킬 ID
    public string skillName;
    public string description;
    public Sprite skillIcon;

    [Header("Cast")]
    public float cooldown; // 디펜스 턴 쿨타임
    public int turnCooldown; // 던전 턴 쿨타임
    public float manaCost;
    public float healthCost;

    [Header("Visual")]
    public GameObject effectPrefab;
    public AnimationClip animationClip;
    public AudioClip soundEffect;

    [Header("Targeting")]
    public SkillTargetingType targetingType; // 스킬 타겟팅 타입
    public float defenseRange;
    public float dungeonRange;

    [Header("Effect")]
    public List<SkillEffect> effects; // 스킬 효과
}