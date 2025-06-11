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

    private IEnemyAi _enemyAiComponent;
    public IEnemyAi EnemyAiComponent => _enemyAiComponent;



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

        _enemyAiComponent = GetComponent<IEnemyAi>();
        if (_enemyAiComponent != null)
        {
            _enemyAiComponent.Initialize(this);
            _enemyAiComponent.OnActionRequest += HandleActionRequest;
        }


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
            _enemyAiComponent.OnActionRequest -= HandleActionRequest;
        }
    }

    // --- 이벤트 핸들러 ---
    private void HandleStatusChanged() { /* ... */ }
    private void HandleSpecificStatusChanged(string statusName, float oldValue, float newValue) 
    {
        _enemyAiComponent.OnStatusChanged(statusName, oldValue, newValue);
    }
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
    private void HandleActionRequest(IActionRequest request)
    {
        if (request == null) return;

        switch (request.Type)
        {
            case ActionType.Deploy:
                var deployRequest = request as DeployActionRequest;
                if (deployRequest != null)
                {
                    _deployableComponent?.Deploy(deployRequest.DeployPosition, Quaternion.identity);
                }
                break;
            case ActionType.Move:
                var moveRequest = request as MoveActionRequest;
                if (moveRequest != null)
                {
                    _movementComponent?.Move(moveRequest.Destination);
                }
                break;
            case ActionType.Attack:
                var attackRequest = request as AttackActionRequest;
                if (attackRequest != null)
                {
                    _attackerComponent?.TryAttack();
                }
                break;
            case ActionType.UseSkill:
#if UNITY_EDITOR
                Debug.Log($"Skill not implemented");
#endif
                break;
            case ActionType.Hit:
#if UNITY_EDITOR
                Debug.Log($"HitAction not implemented");
#endif
                break;
            case ActionType.Idle:
#if UNITY_EDITOR
                Debug.Log($"IdleAction not implemented");
#endif
                break;
            case ActionType.Die:
#if UNITY_EDITOR
                Debug.Log($"DieAction not implemented");
#endif
                break;
            default:
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