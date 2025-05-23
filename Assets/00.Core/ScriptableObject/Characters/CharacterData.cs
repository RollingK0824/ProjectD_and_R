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

    // ���� ������ = ���ݷ� * (1 - ���� ������)
    // ���� ������ = ���� / (���� + K)
    // k�� ����� �뷱�� ������ �������� ������ ��
    public float physicalDefense = 10f; // ���� 
    public float magicalResistance = 10f; // ���� ���׷�

    public float attackDamage = 10f;
    public float attackSpeed = 1f; // AttackCooldown = 1f / attackSpeed 1ȸ ������ attackCooldown�� �Ŀ� ���� ����
    public float moveSpeed = 5f;

    [Header("Visuals")]
    public GameObject characterPrefab; // ĳ������ �ִϸ����� ������
}