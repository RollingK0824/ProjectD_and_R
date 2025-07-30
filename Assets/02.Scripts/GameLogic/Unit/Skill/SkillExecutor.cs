using ProjectD_and_R.Enums;
using System.Collections.Generic;
using UnityEngine;

public class SkillExecutor
{
    private readonly Dictionary<ProjectD_and_R.Enums.SkillEffectType, ISkillEffectStrategy> _strategies;

    public SkillExecutor()
    {
        _strategies = new Dictionary<ProjectD_and_R.Enums.SkillEffectType, ISkillEffectStrategy>
        {
            { ProjectD_and_R.Enums.SkillEffectType.PhysicalDamage, new PhysicalDamageEffectStrategy() },
            { ProjectD_and_R.Enums.SkillEffectType.MagicalDamage, new MagicalDamageEffectStrategy() },
            { ProjectD_and_R.Enums.SkillEffectType.TrueDamage, new TrueDamageEffectStrategy() },
            { ProjectD_and_R.Enums.SkillEffectType.Heal, new HealEffectStrategy() },
        };
    }

    public void Execute(SkillData skillData, ICharacterCore caster, ICharacterCore optionalTarget = null)
    {
        List<ICharacterCore> targets = ResolveTargets(skillData.targetingType, caster, optionalTarget);

        foreach (var effect in skillData.effects)
        {
            if (_strategies.TryGetValue(effect.effectType, out var strategy))
            {
                foreach (var target in targets)
                {
                    strategy.Apply(caster, target, effect);
                }
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[{this}] / No Strategy Found for Effect Type: {effect.effectType}");
#endif
            }
        }
    }

    private List<ICharacterCore> ResolveTargets(SkillTargetingType targetingType, ICharacterCore caster, ICharacterCore optionalTarget = null)
    {
        List<ICharacterCore> targets = new List<ICharacterCore>();

        switch (targetingType)
        {
            case SkillTargetingType.None:
                break;
            case SkillTargetingType.Self:
                targets.Add(caster);
                break;
            case SkillTargetingType.SingleTarget:
                if (optionalTarget != null)
                {
                    targets.Add(optionalTarget);
                }
                break;
            case SkillTargetingType.AreaOfEffect:
                break;
            case SkillTargetingType.RandomTarget:
                break;
            case SkillTargetingType.Line:
                break;
            case SkillTargetingType.Circle:
                break;
            case SkillTargetingType.Cone:
                break;
            default:
                break;
        }

        return targets;
    }

    private List<ICharacterCore> FindTargetsInRange(ICharacterCore caster, float radius)
    {
        //Vector3 casterPos = caster.GameObject.transform.position;

        return new List<ICharacterCore>();
    }
}