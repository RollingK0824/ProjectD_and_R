using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitData", menuName = "ScriptableObjects/Units")]
public class UnitData : ScriptableObject
{
    public int id;
    public string Name;
    public bool Lock = false;
    public EnemyCharacterData Unit;
}
