// Assets/00.Core/ScriptableObjects/Characters/PlayerCharacterData.cs
using UnityEngine;
using ProjectD_and_R.Enums;

[CreateAssetMenu(fileName = "NewPlayerCharacterData", menuName = "ScriptableObjects/Character/PlayerCharacterData")]
public class PlayerCharacterData : CharacterData
{
    [Header("Player Specific")]
    public int level = 0;
    public float experience = 0f;
    public int tier = 0;
    public CharacterRarity rarity = CharacterRarity.None;
}