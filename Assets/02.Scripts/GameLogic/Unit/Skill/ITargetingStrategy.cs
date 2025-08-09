using ProjectD_and_R.Enums;
using System.Collections.Generic;

public interface ITargetingStrategy
{
    SkillTargetingType TargetingType { get; }
    List<ICharacterCore> Resolve(SkillData skillData, ICharacterCore caster, ICharacterCore optionalTarget = null);
}
