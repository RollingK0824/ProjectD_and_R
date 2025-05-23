using UnityEngine;
using System;

public abstract class CharacterCore : MonoBehaviour
{
    [Header("Character Data")]
    [SerializeField] private CharacterData _characterData;
    public CharacterData CharacterData => _characterData; // getter

    // ��Ÿ�� ĳ���� ������ ����
    protected float maxHealth; // ���� �� ���� ��뿡 �������� ���Ͽ� maxHealth�� ��Ÿ�� ������ ����
    protected float currentHealth; // ���� ü��
    protected float currentPhysicalDefense; // ���� ���� ����
    protected float currentMagicalResitance; // ���� ���� ���׷�
    protected float currentAttackDamage; // ���� ���ݷ�
    protected float currentAttackSpeed; // ���� ���� �ӵ�
    protected float currentMoveSpeed; // ���� �̵� �ӵ�
    protected float attackCoolDown; // ���� ���� ��Ÿ�� = 1f / attackSpeed
    
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