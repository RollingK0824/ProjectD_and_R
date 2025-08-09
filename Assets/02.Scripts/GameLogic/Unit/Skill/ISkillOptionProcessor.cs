using Unity.VisualScripting;
using UnityEngine;

public interface ISkillOptionProcessor
{
    void Apply(ICharacterCore caster, ICharacterCore target, SkillEffect effect, SkillOption option);
}
