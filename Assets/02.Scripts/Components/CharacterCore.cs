// Assets/02.Scripts/Characters/CharacterCore.cs
using UnityEngine;
using System;
using ProjectD_and_R.Enums;
using System.Collections;
using UnityEngine.AI;
using Unity.Behavior;

public class CharacterCore : MonoBehaviour, ICharacterCore
{
    [Header("Character Data")]
    [SerializeField] private CharacterData _characterData;
    public CharacterData Data => _characterData;

    // ----- 인터페이스 ----- //
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

    private IGridObject _gridObject;
    public IGridObject GridObject => _gridObject;

    private NavMeshAgent _navMeshAgent;
    public NavMeshAgent NavMeshAgent => _navMeshAgent;

    private BehaviorGraphAgent _behaviorGraphAgent;
    public BehaviorGraphAgent BehaviorGraphAgent => _behaviorGraphAgent;


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
    [SerializeField] private ObjectType debug_ObjectType;

    public event System.Action OnCharacterDied;

    protected virtual void Awake()
    {
        if (_characterData == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"CharacterData is not assigned to {gameObject.name}", this);
#endif
            return;
        }

        Initialize();
    }

    void Initialize()
    {
        _characterStatus = new CharacterStatus();
        TryGetComponent<IDamageable>(out _damageableComponent);
        TryGetComponent<IMovable>(out _movementComponent);
        TryGetComponent<IAttacker>(out _attackerComponent);
        TryGetComponent<IDeployable>(out _deployableComponent);
        TryGetComponent<IEnemyAi>(out _enemyAiComponent);
        TryGetComponent<NavMeshAgent>(out _navMeshAgent);
        TryGetComponent<BehaviorGraphAgent>(out _behaviorGraphAgent);

        RegisterEvents();


        if (_damageableComponent != null)
        {
            _damageableComponent.Initialize(this);
        }

        if (_movementComponent != null)
        {
            _movementComponent.Initialize(this);
        }

        if (_attackerComponent != null)
        {
            _attackerComponent.Initialize(this);
        }

        if (_deployableComponent != null)
        {
            _deployableComponent.Initialize(this);
        }

        if (_enemyAiComponent != null)
        {
            _enemyAiComponent.Initialize(this);
        }
        
        _characterStatus.Initialize(this);
    }

    void OnEnable()
    {
        RegisterEvents();
    }
    void OnDisable()
    {
        UnRegisterEvents();
    }

    // --- 이벤트 핸들러 ---
    private void RegisterEvents()
    {
        if (_characterStatus != null)
        {
            _characterStatus.OnStatusChanged += HandleStatusChanged;
            _characterStatus.OnSpecificStatusChanged += HandleSpecificStatusChanged;
            _characterStatus.OnSpecificStatusChanged += HandleSpecificStatusChangedForDebug;
        }

        if (_deployableComponent != null)
        {
            _deployableComponent.OnDeployed += HandleCharacterDeployed;
            _deployableComponent.OnUnDeployed += HandleCharacterUndeployed;
        }

        if (_attackerComponent != null) _attackerComponent.OnAttackHit += HandleAttackHit;

        if (_damageableComponent != null) _damageableComponent.OnDied += HandleCharacterDied;

        if (_enemyAiComponent != null) _enemyAiComponent.OnActionRequest += HandleActionRequest;

        if (_movementComponent != null) { /* Do Nothing */ }
    }
    private void UnRegisterEvents()
    {
        if (_characterStatus != null)
        {
            _characterStatus.OnStatusChanged -= HandleStatusChanged;
            _characterStatus.OnSpecificStatusChanged -= HandleSpecificStatusChanged;
            _characterStatus.OnSpecificStatusChanged -= HandleSpecificStatusChangedForDebug;
        }

        if (_deployableComponent != null)
        {
            _deployableComponent.OnDeployed -= HandleCharacterDeployed;
            _deployableComponent.OnUnDeployed -= HandleCharacterUndeployed;
        }

        if (_attackerComponent != null) _attackerComponent.OnAttackHit -= HandleAttackHit;

        if (_damageableComponent != null) _damageableComponent.OnDied -= HandleCharacterDied;

        if (_enemyAiComponent != null) _enemyAiComponent.OnActionRequest -= HandleActionRequest;

        if (_movementComponent != null) { /* Do Nothing */ }
    }

    // --- 이벤트 핸들러 --- //
    private void HandleStatusChanged() { /* ... */ }
    private void HandleStatusChanged<T>(string statusName, T newValue) { }
    private void HandleSpecificStatusChanged(string statusName, float oldValue, float newValue)
    {
        if (_enemyAiComponent == null) return;
        _enemyAiComponent.StatusChanged(statusName, oldValue, newValue);
    }
    private void HandleCharacterDied() { /* ... */ }
    private void HandleAttackHit(IDamageable target) { /* ... */ }
    private void HandleCharacterDeployed() 
    { 
        CharacterStatus.Initialize(this);
        CharacterStatus.SetIsDeployed(true);

        if(EnemyAiComponent != null)
        {
            EnemyAiComponent.StatusChanged<bool>("IsDeployed", true, true);
            EnemyAiComponent.StatusChanged<bool>("IsAlive", true, true);
        }
    }
    private void HandleCharacterUndeployed() { CharacterStatus.SetIsDeployed(false); }
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

        switch (request)
        {
            case DeployActionRequest:
                var deployRequest = request as DeployActionRequest;
                if (deployRequest != null)
                {
                    _deployableComponent?.Deploy(deployRequest.DeployPosition, Quaternion.identity);
                }
                break;
            case MoveActionRequest:
                var moveRequest = request as MoveActionRequest;
                if (moveRequest != null)
                {
                    _movementComponent?.Move(moveRequest.Destination);
                }
                break;
            case MoveStopActionRequest:
                var stopRequest = request as MoveStopActionRequest;
                if(stopRequest != null)
                {
                    _movementComponent?.StopMoving();
                }
                break;
            case AttackActionRequest:
                var attackRequest = request as AttackActionRequest;
                if (attackRequest != null)
                {
                    if(attackRequest.Target != null)
                    {
                        _attackerComponent?.TryAttack(attackRequest.Target);
                    }
                    else
                    {
                        _attackerComponent?.TryAttack();
                    }
                }
                break;
            case UseSkillActionRequest:
#if UNITY_EDITOR
                Debug.Log($"Skill not implemented");
#endif
                break;
            case HitActionRequest:
#if UNITY_EDITOR
                Debug.Log($"HitAction not implemented");
#endif
                break;
            default:
                break;
#if UNITY_EDITOR
                Debug.Log($"IdleAction not implemented");
#endif

#if UNITY_EDITOR
                Debug.Log($"DieAction not implemented");
#endif
        }
    }

    // --- 외부 호출 메서드 --- //
    public void ReceiveDamage(float rawDamage, DamageType damageType)
    {
        if (_damageableComponent != null)
        {
            _damageableComponent.TakeDamage(rawDamage, damageType);
        }
    }
    public void MoveCharacterTo(Vector3 targetPosition) => _movementComponent?.Move(targetPosition);
    public void Attack() => _attackerComponent?.TryAttack();
    public void DeployCharacter(Vector3 position, Quaternion rotation) => _deployableComponent?.Deploy(position, rotation);
}