// Assets/00.Core/ScriptableObjects/Characters/EnemyCharacterData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/Character/EnemyData")]
public class EnemyCharacterData : CharacterData
{
    [Header("Enemy Specific")]
    public int experienceOnDefeat = 10;
    public int goldOnDefeat = 5;
    public EnemyType enemyType; // Enum: NORMAL, ELITE, BOSS 등
    public float detectionRange = 10f;
}

public enum EnemyType { Normal, Elite, Boss }