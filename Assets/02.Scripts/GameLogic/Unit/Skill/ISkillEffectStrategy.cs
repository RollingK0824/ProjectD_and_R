using UnityEngine;

public interface ISkillEffectStrategy 
{
    void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect);
}
