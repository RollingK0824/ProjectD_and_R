// Assets/02.Scripts/Components/Common/DamageableComponent.cs
using ProjectD_and_R.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableComponent : MonoBehaviour, IDamageable
{
    private CharacterCore _characterCore;

    public event Action<float> OnHealthChanged;
    public event Action OnDied;

    public void Initialize(CharacterCore characterCore)
    {
        if (characterCore == null) return;
        _characterCore = characterCore;

        if (_characterCore?.Status != null)
        {
            _characterCore.Status.OnSpecificStatusChanged += HandleStatusChange;
        }
    }

    private void HandleStatusChange(string statusName, float oldValue, float newValue)
    {
        if (statusName == nameof(ICharacterStatus.CurrentHealth))
        {
            OnHealthChanged?.Invoke(newValue);
            if (newValue <= 0 && oldValue > 0)
            {
                OnDied?.Invoke();
            }
        }
    }

    public void TakeDamage(float rawDamage, DamageType damageType)
    {
        if (_characterCore == null
            || _characterCore.Status == null
            || !_characterCore.Status.IsAlive) return;

        float finalDamage = rawDamage; // 초기화

        switch (damageType)
        {
            case DamageType.Pyhsical:
                float physicalDefense = _characterCore.Status.PhysicalDefense;
                finalDamage = rawDamage * (1 - (physicalDefense / (physicalDefense + 200f)));
                break;
            case DamageType.Magical:
                float magicalResistance = _characterCore.Status.MagicalResistance;
                finalDamage = rawDamage * (1 - (magicalResistance / (magicalResistance + 200f)));
                break;
            case DamageType.TrueDamage:
                finalDamage = rawDamage;    // 고정 데미지 (방어력 무시)
                break;
            default:
#if UNITY_EDITOR
                Debug.LogWarning($"Unkown DamageType : {damageType}.", this);
#endif
                break;
        }

        finalDamage = Mathf.Max(rawDamage * 0.1f, finalDamage);   // 최소 데미지 = 데미지의 10%

        _characterCore.Status.SetCurrentHealth(_characterCore.Status.CurrentHealth - finalDamage);


#if UNITY_EDITOR
        Debug.Log($"{_characterCore.Data.CharacterName}이{finalDamage}를 입음");
#endif
    }
    public void Heal(float amount)
    {
        if (_characterCore == null
            || _characterCore.Status == null
            || !_characterCore.Status.IsAlive
            || amount < 0) return;

        _characterCore.Status.SetCurrentHealth(_characterCore.Status.CurrentHealth + amount);
#if UNITY_EDITOR
        Debug.Log($"{_characterCore.Data.CharacterName}이 {amount}만큼 체력이 회복됨");
#endif
    }
    public void Die()
    {
        /* 사망 처리 */
    }
}