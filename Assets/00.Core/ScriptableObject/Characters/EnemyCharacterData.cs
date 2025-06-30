// Assets/00.Core/ScriptableObjects/Characters/EnemyCharacterData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/Character/EnemyData")]
public class EnemyCharacterData : CharacterData
{
    [Header("Enemy Specific")]
    public int id;
    public string Name;
    public bool Lock = false;
    public int experienceOnDefeat = 10;
    public int goldOnDefeat = 5;
    public EnemyType Race = EnemyType.Default;
    public float detectionRange = 10f;
}
