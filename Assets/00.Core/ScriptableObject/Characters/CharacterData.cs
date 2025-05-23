// Assets/00.Core/ScriptableObjects/Characters/CharacterData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/Character/CharacterData")]
public abstract class CharacterData : ScriptableObject
{
    [Header("Basic Info")]
    public string characterName = "New Character";
    public Sprite characterIcon;
    public string description = "";

    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    // 최종 데미지 = 공격력 * (1 - 방어력 감소율)
    // 방어력 감소율 = 방어력 / (방어력 + K)
    // k는 상수로 밸런싱 조절을 목적으로 조절할 값
    public float physicalDefense = 10f; // 방어력 
    public float magicalResistance = 10f; // 마법 저항력

    public float attackDamage = 10f;
    public float attackSpeed = 1f; // AttackCooldown = 1f / attackSpeed 1회 공격후 attackCooldown초 후에 공격 가능
    public float moveSpeed = 5f;

    [Header("Visuals")]
    public GameObject characterPrefab; // 캐릭터의 애니메이터 프리팹
}