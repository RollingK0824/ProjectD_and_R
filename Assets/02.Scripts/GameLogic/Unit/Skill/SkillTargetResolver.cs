using ProjectD_and_R.Enums;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargetResolver : ISkillTargetResolver
{
    public List<ICharacterCore> Resolve(SkillTargetingType targetingType, ICharacterCore caster, ICharacterCore optionalTarget = null)
    {
        throw new System.NotImplementedException();
    }
}
