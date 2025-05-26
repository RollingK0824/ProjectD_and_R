// Assets/02.Scripts/Common/CharacterStatus.cs
using ProjectD_and_R.Enums;
using System;
using UnityEngine; // CharacterData를 사용하기 위해 필요

public class CharacterStatus : ICharacterStatus
{
    public event Action OnStatusChanged;
    public event Action<string, float, float> OnSpecificStatusChanged;

    private float _currentHealth;
    public float CurrentHealth // 현재 체력도 CharacterStatus에서 관리
    {
        get => _currentHealth;
        private set
        {
            if (_currentHealth != value)
            {
                float oldValue = _currentHealth;
                _currentHealth = Mathf.Clamp(value, 0, MaxHealth); // 0과 MaxHealth 사이로 클램프
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(CurrentHealth), oldValue, _currentHealth);
            }
        }
    }

    private float _maxHealth;
    public float MaxHealth
    {
        get => _maxHealth;
        private set
        {
            if (value != _maxHealth)
            {
                float oldValue = _maxHealth;
                _maxHealth = value;
                // 최대 체력 변경 시 현재 체력도 조절
                CurrentHealth = Mathf.Min(CurrentHealth, _maxHealth);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(MaxHealth), oldValue, value);
            }
        }
    }

    private float _physicalDefense;
    public float PhysicalDefense
    {
        get => _physicalDefense;
        private set
        {
            if (value != _physicalDefense)
            {
                float oldValue = _physicalDefense;
                _physicalDefense = value;
                _physicalDefense = Mathf.Clamp(value, 0, _physicalDefense);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(PhysicalDefense), oldValue, value);
            }
        }
    }

    private float _magicalResistance;
    public float MagicalResistance
    {
        get => _magicalResistance;
        private set
        {
            if (value != _magicalResistance)
            {
                float oldValue = _magicalResistance;
                _magicalResistance = value;
                _magicalResistance = Mathf.Clamp(value, 0, _magicalResistance);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(MagicalResistance), oldValue, value);
            }
        }
    }

    private float _attackDamage;
    public float AttackDamage
    {
        get => _attackDamage;
        private set
        {
            if (value != _attackDamage)
            {
                float oldValue = _attackDamage;
                _attackDamage = value;
                _attackDamage = Mathf.Clamp(value, 0, _attackDamage);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(AttackDamage), oldValue, value);
            }
        }
    }

    private float _attackSpeed;
    public float AttackSpeed
    {
        get => _attackSpeed;
        private set
        {
            if (value != _attackSpeed)
            {
                float oldValue = _attackSpeed;
                _attackSpeed = value;
                _attackSpeed = Mathf.Clamp(value, 0, _attackSpeed);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(AttackSpeed), oldValue, value);
            }
        }
    }

    private float _attackRange;
    public float AttackRange
    {
        get => _attackRange;
        private set
        {
            if (value != _attackRange)
            {
                float oldValue = _attackRange;
                _attackRange = value;
                _attackRange = Math.Clamp(value, 0, _attackRange);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(AttackRange), oldValue, value);
            }
        }
    }

    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        private set
        {
            if (value != _moveSpeed)
            {
                float oldValue = _moveSpeed;
                _moveSpeed = value;
                _moveSpeed = Mathf.Clamp(value, 0, _moveSpeed);
                OnStatusChanged?.Invoke();
                OnSpecificStatusChanged?.Invoke(nameof(MoveSpeed), oldValue, value);
            }
        }
    }

    private bool _isAlive;
    public bool IsAlive
    {
        get => _isAlive;
        private set
        {
            if (value != _isAlive)
            {
                _isAlive = value;
            }

        }
    }

    private MoveType _moveType;
    public MoveType MovableTerrainTypes
    {
        get => _moveType;
        private set
        {
            if (value != _moveType)
            {
                _moveType = value;
            }
        }
    }

    private Faction _faction;
    public Faction Faction
    {
        get => _faction;
        private set
        {
            if (value != _faction)
            {
                _faction = value;
            }
        }
    }

    // 초기화 메서드 (CharacterData로부터 베이스 스탯을 받음)
    public void Initialize(CharacterCore characterCore)
    {
        CharacterData baseData = characterCore.Data;

        MaxHealth = baseData.MaxHealth;
        CurrentHealth = MaxHealth; // 초기 체력은 최대 체력과 동일

        PhysicalDefense = baseData.PhysicalDefense;
        MagicalResistance = baseData.MagicalResistance;
        AttackDamage = baseData.AttackDamage;
        AttackSpeed = baseData.AttackSpeed;
        AttackRange = baseData.AttackRange;
        MoveSpeed = baseData.MoveSpeed;

        IsAlive = CurrentHealth > 0;

        OnStatusChanged?.Invoke(); // 초기화 완료 시 전체 변경 이벤트 발생
#if UNITY_EDITOR
        Debug.Log("CharacterStatus Initialized with base data.");
#endif
    }

    // 런타임에 스탯을 변경하는 public 메서드 (CharacterCore, 버프 시스템 등에서 호출)
    public void SetCurrentHealth(float value) => CurrentHealth = value;
    public void SetMaxHealth(float value) => MaxHealth = value;
    public void SetPhysicalDefense(float value) => PhysicalDefense = value;
    public void SetAttackDamage(float value) => AttackDamage = value;
    public void SetAttackSpeed(float value) => AttackSpeed = value;
    public void SetAttackRange(float value) => AttackRange = value;
    public void SetMoveSpeed(float value) => MoveSpeed = value;
    public void SetFaction(Faction faction) => Faction = faction;
    public void SetMoveType(MoveType moveType) => MovableTerrainTypes = moveType;
}