using ProjectD_and_R.Enums;
using System;

public interface ICharacterStatus
{
    float MaxHealth { get; }
    float CurrentHealth { get; }
    float PhysicalDefense { get; }
    float MagicalResistance { get; }
    float AttackDamage { get; }
    float AttackSpeed { get; }
    float AttackRange { get; }
    float MoveSpeed { get; }
    bool IsAlive { get; }
    bool IsDeployed { get; }

    MoveType MovableTerrainTypes { get; }
    Faction Faction { get; }

    public void SetCurrentHealth(float value);
    public void SetMaxHealth(float value);
    public void SetPhysicalDefense(float value);
    public void SetAttackDamage(float value);
    public void SetAttackSpeed(float value);
    public void SetMoveSpeed(float value);
    public void SetIsDeployed(bool value);


    event Action OnStatusChanged;
    event Action<string, float, float> OnSpecificStatusChanged;

    void Initialize(ICharacterCore characterCore);
}
