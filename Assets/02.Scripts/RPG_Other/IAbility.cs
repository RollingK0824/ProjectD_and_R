using UnityEngine;

public abstract class IAbility : ScriptableObject
{
    public virtual void OnSingleTarget(CharacterCore owner, CharacterCore target) { }
    public virtual void OnMultiTarget(CharacterCore owner, CharacterCore target) { }

}

