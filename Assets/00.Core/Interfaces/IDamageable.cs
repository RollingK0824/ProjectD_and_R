// Assets/00.Core/Interfaces/IDamageable.cs
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float rawDamage);   // 데미지를 받는 함수
    void Heal(float amount);    // 체력 회복 함수
    void Die(); // 사망 함수
}
