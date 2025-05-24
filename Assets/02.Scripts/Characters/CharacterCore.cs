using UnityEngine;
using System;

public abstract class CharacterCore : MonoBehaviour
{
    [Header("Character Data")]
    [SerializeField] private CharacterData _characterData;
    public CharacterData CharacterData => _characterData; // getter

    // 런타임 캐릭터 데이터 변수
    protected float maxHealth; // 버프 등 추후 사용에 유연함을 위하여 maxHealth도 런타임 변수로 관리
    protected float currentHealth; // 현재 체력
    protected float currentPhysicalDefense; // 현재 물리 방어력
    protected float currentMagicalResitance; // 현재 마법 저항력
    protected float currentAttackDamage; // 현재 공격력
    protected float currentAttackSpeed; // 현재 공격 속도
    protected float currentMoveSpeed; // 현재 이동 속도
    protected float attackCoolDown; // 공격 쿨타임 = 1f / attackSpeed
    
    protected virtual void Awake()
    {
        if(_characterData == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"CharacterData is not assigned to {gameObject.name}", this);
#endif
            return;
        }
        maxHealth = _characterData.maxHealth;
        currentHealth = _characterData.currentHealth;
        currentPhysicalDefense = _characterData.physicalDefense;
        currentMagicalResitance = _characterData.magicalResistance;
        currentAttackDamage = _characterData.attackDamage;
        currentAttackSpeed = _characterData.attackSpeed;
        currentMoveSpeed = _characterData.moveSpeed;

        if(currentAttackSpeed > 0)
        {
            attackCoolDown = 1f / currentAttackSpeed;
        }
        else
        {
            attackCoolDown = 0.3f;
        }
    }
}