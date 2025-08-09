using ProjectD_and_R.Enums;
using UnityEngine;

public class DamageEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        if (caster == null || target == null || effect == null)
        {
            float damageAmount = effect.amount;
            DamageType damageType = effect.damageType;

            target.DamageableComponent.TakeDamage(damageAmount, damageType);
        }
    }
}

public class PhysicalDamageEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        if (caster == null || target == null || effect == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[{this}] / Invalid Value");
#endif
            return;
        }

        float damageAmount = effect.amount;

        target.DamageableComponent.TakeDamage(damageAmount, ProjectD_and_R.Enums.DamageType.Physical);
    }
}

public class MagicalDamageEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        if (caster == null || target == null || effect == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[{this}] / Invalid Value");
#endif
            return;
        }

        float damageAmount = effect.amount;

        target.DamageableComponent.TakeDamage(damageAmount, ProjectD_and_R.Enums.DamageType.Magical);
    }
}

public class TrueDamageEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        if (caster == null || target == null || effect == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[{this}] / Invalid Value");
#endif
            return;
        }

        float damageAmount = effect.amount;

        target.DamageableComponent.TakeDamage(damageAmount, ProjectD_and_R.Enums.DamageType.TrueDamage);
    }
}

public class HealEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        if (caster == null || target == null || effect == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[{this}] / Invalid Value");
#endif
            return;
        }

        float healAmount = effect.amount;

        target.DamageableComponent.Heal(healAmount);
    }
}

public class BuffEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        if (caster == null || target == null || effect == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"[{this}] / Invalid Value");
#endif
            return;
        }

        float buffAmount = effect.amount;

        switch (effect.targetStat)
        {
            case ProjectD_and_R.Enums.SkillTargetStatType.None:
#if UNITY_EDITOR
                Debug.LogWarning($"[{this}] / Invalid TargetStatType");
#endif
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.MaxHealth:
                target.CharacterStatus.SetMaxHealth(target.CharacterStatus.MaxHealth + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.CurrentHealth:
                target.CharacterStatus.SetCurrentHealth(target.CharacterStatus.CurrentHealth + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.PhysicalDefense:
                target.CharacterStatus.SetPhysicalDefense(target.CharacterStatus.PhysicalDefense + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.MagicalResistance:
                target.CharacterStatus.SetMagicalResistance(target.CharacterStatus.MagicalResistance + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.AttackDamage:
                target.CharacterStatus.SetAttackDamage(target.CharacterStatus.AttackDamage + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.AttackSpeed:
                target.CharacterStatus.SetAttackSpeed(target.CharacterStatus.AttackSpeed + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.AttackRange:
                target.CharacterStatus.SetAttackRange(target.CharacterStatus.AttackRange + buffAmount);
                break;
            case ProjectD_and_R.Enums.SkillTargetStatType.MoveSpeed:
                target.CharacterStatus.SetMoveSpeed(target.CharacterStatus.MoveSpeed + buffAmount);
                break;
            default:
                break;
        }

    }
}

public class DebuffEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        throw new System.NotImplementedException();
    }
}

public class ShieldEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        throw new System.NotImplementedException();
    }
}

public class StunEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        throw new System.NotImplementedException();
    }
}

public class SummonEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        throw new System.NotImplementedException();
    }
}

public class TeleportEffectStrategy : ISkillEffectStrategy
{
    public void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect)
    {
        throw new System.NotImplementedException();
    }
}