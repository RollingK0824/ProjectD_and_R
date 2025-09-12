// Assets/00.Core/Interfaces/IAttacker.cs
using System;
using UnityEngine;

public interface IAttacker
{
    /// <summary>
    /// 공격 시도 함수
    /// </summary>
    void TryAttack();

    /// <summary>
    /// 공격 시도 함수
    /// </summary>
    /// <param name="target">공격 대상</param>
    void TryAttack(GameObject target);

    /// <summary>
    /// 현재 공격중인지 여부
    /// </summary>
    bool IsAttacking { get; }

    /// <summary>
    /// 공격 성공 이벤트
    /// </summary>
    event Action<IDamageable> OnAttackHit;

    /// <summary>
    /// CharacterCore로부터 초기 스탯을 받아 초기화 하는 함수
    /// </summary>
    /// <param name="characterCore">캐릭터 코어</param>
    void Initialize(ICharacterCore characterCore);
}
