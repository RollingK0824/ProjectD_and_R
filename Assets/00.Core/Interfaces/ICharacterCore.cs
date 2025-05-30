using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterCore
{
    CharacterData Data { get; }
    ICharacterStatus CharacterStatus { get; }
    IDamageable DamageableComponent { get; }
    IMovable MovementComponent { get; }
    IAttacker AttackerComponent { get; }
    IDeployable DeployableComponent { get; }
}
