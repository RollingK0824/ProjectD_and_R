using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterTypeDataData", menuName = "ScriptableObjects/Character/CharacterTypeData")]
public class CharacterTypeData : ScriptableObject
{
    public string typeName;
    public CharacterRaceData raceData;
    public Sprite typeIcon;
    public string typeDescription;
}
