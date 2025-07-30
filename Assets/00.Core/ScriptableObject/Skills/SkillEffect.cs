
using ProjectD_and_R.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillData", menuName = "ScriptableObjects/Skill/SKillEffect")]
public class SkillEffect : ScriptableObject
{
    public SkillEffectType effectType;
    public SkillTargetStatType targetStat;
    public float amount;
    public float duration;
}