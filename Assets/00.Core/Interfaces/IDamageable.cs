// Assets/00.Core/Interfaces/IDamageable.cs
using ProjectD_and_R.Enums;
using System;

public interface IDamageable
{
    /// <summary>
    /// 데미지를 받는 함수
    /// </summary>
    /// <param name="rawDamage">데미지</param>
    /// <param name="damageType">데미지 타입(물리, 마법, 고정)</param>
    void TakeDamage(float rawDamage, DamageType damageType);

    /// <summary>
    /// 체력 회복 함수
    /// </summary>
    /// <param name="amount">회복 수치</param>
    void Heal(float amount);

    /// <summary>
    /// 사망 함수
    /// </summary>
    void Die();

    /// <summary>
    /// 사망 시 호출 이벤트
    /// </summary>
    event Action OnDied;

    /// <summary>
    /// 초기 스탯 초기화 함수
    /// </summary>
    /// <param name="characterCore">캐릭터 코어</param>
    void Initialize(ICharacterCore characterCore);
}
