using NUnit.Framework;
using ProjectD_and_R.Enums;
using UnityEngine;
using System.Collections.Generic;

public interface ISkillTargetResolver
{
    List<ICharacterCore> Resolve(SkillTargetingType targetingType, ICharacterCore caster, ICharacterCore optionalTarget = null);
}
