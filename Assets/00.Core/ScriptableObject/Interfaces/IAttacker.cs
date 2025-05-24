// Assets/00.Core/Interfaces/IAttacker.cs
using UnityEngine;

public interface IAttacker
{
    void TryAttack();   // 공격 시도 함수
    void PerformAttack();   // 공격 시도 시 IDamageable을 가진 대상을 찾아 실제 공격을 하는 함수

    bool bIsAttacking { get;}   // 현재 공격 중인지 여부
    float CurrentAttackDamage { get;}   // 현재 공격력
    float AttackRange { get;}   // 공격 사거리
}
