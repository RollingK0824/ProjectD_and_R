// Assets/02.Scripts/Characters/CharacterCore.cs
using UnityEngine;
using System;
using ProjectD_and_R.Enums;
using System.Collections;

public class CharacterCore : MonoBehaviour, ICharacterCore
{
    [Header("Character Data")]
    [SerializeField] private CharacterData _characterData;
    public CharacterData Data => _characterData;


    private ICharacterStatus _characterStatus;
    public ICharacterStatus CharacterStatus => _characterStatus;

    private IDamageable _damageableComponent;
    public IDamageable DamageableComponent => _damageableComponent;

    private IMovable _movementComponent;
    public IMovable MovementComponent => _movementComponent;

    private IAttacker _attackerComponent;
    public IAttacker AttackerComponent => _attackerComponent;

    private IDeployable _deployableComponent;
    public IDeployable DeployableComponent => _deployableComponent;



    [Header("Debug Status (Read Only")]
    [SerializeField] private float debug_MaxHealth;
    [SerializeField] private float debug_CurrentHealth;
    [SerializeField] private float debug_PhysicalDefense;
    [SerializeField] private float debug_MagicalResistance;
    [SerializeField] private float debug_AttackDamage;
    [SerializeField] private float debug_AttackSpeed;
    [SerializeField] private float debug_AttackRange;
    [SerializeField] private float debug_MoveSpeed;
    [SerializeField] private bool debug_IsAlive;
    [SerializeField] private MoveType debug_MovableTerrainTypes;
    [SerializeField] private Faction debug_Faction;

    public event Action OnCharacterDied;

    public GameObject[] testMoveToTarget;

    protected virtual void Awake()
    {
        if (_characterData == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"CharacterData is not assigned to {gameObject.name}", this);
#endif
            return;
        }

        _characterStatus = new CharacterStatus();

        _characterStatus.OnStatusChanged += HandleStatusChanged;
        _characterStatus.OnSpecificStatusChanged += HandleSpecificStatusChanged;
        _characterStatus.OnSpecificStatusChanged += HandleSpecificStatusChangedForDebug;

        _characterStatus.Initialize(this);

        _damageableComponent = GetComponent<IDamageable>();
        if (_damageableComponent != null)
        {
            _damageableComponent.Initialize(this);
            _damageableComponent.OnDied += HandleCharacterDied;
        }

        _movementComponent = GetComponent<IMovable>();
        if (_movementComponent != null)
        {
            _movementComponent.Initialize(this);
        }

        _attackerComponent = GetComponent<IAttacker>();
        if (_attackerComponent != null)
        {
            _attackerComponent.Initialize(this);
            _attackerComponent.OnAttackHit += HandleAttackHit;
        }

        _deployableComponent = GetComponent<IDeployable>();
        if (_deployableComponent != null)
        {
            _deployableComponent.Initialize(this);
            _deployableComponent.OnDeployed += HandleCharacterDeployed;
            _deployableComponent.OnUnDeployed += HandleCharacterUndeployed;
        }

        StartCoroutine(TestMove());
    }

    IEnumerator TestMove()
    {
        yield return new WaitForSeconds(1);
        _movementComponent.Move(testMoveToTarget[0].transform.position);
    }

    public void Attack()
    {
        _attackerComponent.TryAttack();
    }

    void OnDisable()
    {
        if (CharacterStatus != null)
        {
            _characterStatus.OnStatusChanged -= HandleStatusChanged;
            _characterStatus.OnSpecificStatusChanged -= HandleSpecificStatusChanged;
            _characterStatus.OnSpecificStatusChanged -= HandleSpecificStatusChangedForDebug;
            _attackerComponent.OnAttackHit -= HandleAttackHit;
            _damageableComponent.OnDied -= HandleCharacterDied;
            _deployableComponent.OnDeployed -= HandleCharacterDeployed;
            _deployableComponent.OnUnDeployed -= HandleCharacterUndeployed;
        }
    }

    // --- 이벤트 핸들러 ---
    private void HandleStatusChanged() { /* ... */ }
    private void HandleSpecificStatusChanged(string statusName, float oldValue, float newValue) { /* ... */ }
    private void HandleCharacterDied() { /* ... */ }
    private void HandleAttackHit(IDamageable target) { /* ... */ }
    private void HandleCharacterDeployed() { /* ... */ }
    private void HandleCharacterUndeployed() { /* ... */ }
    private void HandleSpecificStatusChangedForDebug(string statusName, float oldValue, float newValue)
    {
        if (CharacterStatus == null) return;

        switch (statusName)
        {
            case nameof(ICharacterStatus.MaxHealth):
                debug_MaxHealth = newValue;
                break;
            case nameof(ICharacterStatus.CurrentHealth):
                debug_CurrentHealth = newValue;
                break;
            case nameof(ICharacterStatus.PhysicalDefense):
                debug_PhysicalDefense = newValue;
                break;
            case nameof(ICharacterStatus.MagicalResistance):
                debug_MagicalResistance = newValue;
                break;
            case nameof(ICharacterStatus.AttackDamage):
                debug_AttackDamage = newValue;
                break;
            case nameof(ICharacterStatus.AttackSpeed):
                debug_AttackSpeed = newValue;
                break;
            case nameof(ICharacterStatus.AttackRange):
                debug_AttackRange = newValue;
                break;
            case nameof(ICharacterStatus.MoveSpeed):
                debug_MoveSpeed = newValue;
                break;
        }

    }

    // --- 외부 호출 메서드 ---
    public void ReceiveDamage(float rawDamage, DamageType damageType)
    {
        if (_damageableComponent != null)
        {
            _damageableComponent.TakeDamage(rawDamage, damageType);
        }
    }
    public void MoveCharacterTo(Vector3 targetPosition) => _movementComponent?.Move(targetPosition);
    public void PerformAttack() => _attackerComponent?.TryAttack();
    public void DeployCharacter(Vector3 position, Quaternion rotation) => _deployableComponent?.Deploy(position, rotation);
}