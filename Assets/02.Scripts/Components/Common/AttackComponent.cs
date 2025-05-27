// Assets/02.Scripts/Components/Common/AttackComponent.cs
using UnityEngine;
using System.Collections;
using System;
using ProjectD_and_R.Enums;

public class AttackComponent : MonoBehaviour, IAttacker
{
    private CharacterCore _characterCore;

    private float _attackCooldownDuration; // 쿨다운 시간
    private float _nextAttackTime = 0f;
    public bool IsAttacking { get; private set; } = false;

    public event Action<IDamageable> OnAttackHit;

    // CharacterCore로부터 초기 스탯을 받아 초기화하는 메서드
    public void Initialize(CharacterCore characterCore)
    {
        if (characterCore == null) return;
        _characterCore = characterCore;

        _attackCooldownDuration = 1f / _characterCore.Status.AttackSpeed;
        
#if UNITY_EDITOR
        Debug.Log($"AttackComponent Initialized: Damage={_characterCore.Status.AttackDamage}, Cooldown={_attackCooldownDuration:F2}s");
#endif
    }

    public void TryAttack()
    {
        if (Time.time >= _nextAttackTime && !IsAttacking)
        {
            //  StartCoroutine(AttackRoutine());
            PerformDamageApplication();
            _nextAttackTime = Time.time + _attackCooldownDuration;
        }
    }

    private IEnumerator AttackRoutine()
    {
        IsAttacking = true;
        Debug.Log($"{gameObject.name} performing attack!");
        yield return new WaitForSeconds(0.1f); // 짧은 딜레이 후 공격 판정

        PerformDamageApplication();
        IsAttacking = false;
    }

    private void PerformDamageApplication()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _characterCore.Status.AttackRange);
        foreach (Collider hitCollider in hitColliders)
        {
            IDamageable damageableTarget = hitCollider.GetComponent<IDamageable>();
            CharacterCore targetCore = hitCollider.GetComponent<CharacterCore>();

            if (damageableTarget != null && targetCore != null && targetCore.gameObject != this.gameObject)
            {
                if (_characterCore.Status.Faction != targetCore.Data.Faction) // 진영이 다를 때만 공격
                {
                    /* 스킬 수행 예정 */
#if UNITY_EDITOR
                    Debug.Log($"{gameObject.GetComponent<CharacterCore>().Data.CharacterName}:{gameObject.GetInstanceID()}이 {targetCore.Data.CharacterName}:{targetCore.GetInstanceID()}을 공격");
                    /* 테스트 용 코드 */
                    damageableTarget.TakeDamage(_characterCore.Status.AttackDamage, DamageType.Pyhsical);
#endif
                }
            }
        }
    }
}