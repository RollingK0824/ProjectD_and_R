using UnityEngine;
using ProjectD_and_R.Enums;
using System.Collections.Generic;
using NUnit.Framework;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObjects/Skill/SKillData")]
public class SkillData : ScriptableObject
{
    [Header("Info")]
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

    [Header("Effect")]
    public List<SkillEffect> effects; // 복수 효과
}

