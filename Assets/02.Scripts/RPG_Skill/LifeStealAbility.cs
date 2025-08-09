using UnityEngine;

[CreateAssetMenu(fileName = "LifeSteal", menuName = "ScriptableObjects/Ability")]
public class LifeStealAbility : IAbility
{
    [Range(0,1)] public float lifestealRatio = 0.1f;

    public override void OnSingleTarget(CharacterCore owner, CharacterCore target)
    {
        
    }
}
