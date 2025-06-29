// Assets/02.Scripts/Components/Common/AttackComponent.cs
using UnityEngine;
using System.Collections;
using System;
using ProjectD_and_R.Enums;

public class AttackComponent : MonoBehaviour, IAttacker
{
    private ICharacterStatus _status;

    private float _attackCooldownDuration; // 쿨다운 시간
    private float _nextAttackTime = 0f;
    public bool IsAttacking { get; private set; } = false;

    public event Action<IDamageable> OnAttackHit;

    // CharacterCore로부터 초기 스탯을 받아 초기화하는 메서드
    public void Initialize(ICharacterCore characterCore)
    {
        if (characterCore == null) return;
        _status = characterCore.CharacterStatus;

        _attackCooldownDuration = 1f / _status.AttackSpeed;

#if UNITY_EDITOR
        Debug.Log($"AttackComponent Initialized: Damage={_status.AttackDamage}, Cooldown={_attackCooldownDuration:F2}s");

#endif
    }

    // --- 임시 로직 --- //
    private void Update()
    {
        if(_status.Faction == Faction.Player)
        {
            TryAttack();
        }
    }

    public void TryAttack()
    {
        if (Time.time >= _nextAttackTime && !IsAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    public void TryAttack(GameObject target)
    {
        if (Time.time >= _nextAttackTime && !IsAttacking)
        {
            StartCoroutine(AttackRoutine(target));
        }
    }

    // 공격에 선딜레이를 줄 때 사용하는 함수
    private IEnumerator AttackRoutine()
    {
        IsAttacking = true;
        Debug.Log($"{gameObject.name} performing attack!");
        yield return new WaitForSeconds(0.15f); // 공격 선딜레이

        _nextAttackTime = Time.time + _attackCooldownDuration;
        PerformDamageApplication();
        IsAttacking = false;
    }

    // 공격에 선딜레이를 줄 때 사용하는 함수
    private IEnumerator AttackRoutine(GameObject target)
    {
        IsAttacking = true;
        Debug.Log($"{gameObject.name} performing attack!");
        yield return new WaitForSeconds(0.15f); // 공격 선딜레이

        _nextAttackTime = Time.time + _attackCooldownDuration;
        PerformDamageApplication(target);
        IsAttacking = false;
    }

    private void PerformDamageApplication()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _status.AttackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            IDamageable damageableTarget = hitCollider.GetComponent<IDamageable>();
            CharacterCore targetCore = hitCollider.GetComponent<CharacterCore>();

            if (damageableTarget != null && targetCore != null && targetCore.gameObject != this.gameObject)
            {

                if (_status.Faction != targetCore.Data.Faction) // 진영이 다를 때만 공격
                {
                    //targetCore.ReceiveDamage(_status.AttackDamage, DamageType.Pyhsical);
                    damageableTarget.TakeDamage(_status.AttackDamage, DamageType.Pyhsical);
                    /* 스킬 수행 예정 */
#if UNITY_EDITOR
                    Debug.Log($"{gameObject.GetComponent<CharacterCore>().Data.CharacterName}:{gameObject.GetInstanceID()}이 {targetCore.Data.CharacterName}:{targetCore.GetInstanceID()}을 공격");
                    /* 테스트 용 코드 */
#endif
                }
            }
        }
    }

    private void PerformDamageApplication(GameObject target)
    {
        IDamageable damageableTarget = target.GetComponent<IDamageable>();
        ICharacterCore targetCore = target.GetComponent<ICharacterCore>();

        if (damageableTarget != null && targetCore != null)
        {
            if (_status.Faction != targetCore.Data.Faction) // 진영이 다를 때만 공격
            {
                damageableTarget.TakeDamage(_status.AttackDamage, DamageType.Pyhsical);
                /* 스킬 수행 예정 */
#if UNITY_EDITOR
                Debug.Log($"{gameObject.GetComponent<CharacterCore>().Data.CharacterName}이 {targetCore.Data.CharacterName}을 공격");
                /* 테스트 용 코드 */
#endif
            }
        }

    }
}