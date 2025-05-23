using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawnData", menuName = "ScriptableObjects/Map/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    public int _Floor;
    [Range(0, 100)] public float _Nomal; 
    [Range(0, 100)] public float _Elite; 
    [Range(0, 100)] public float _Event; 
    [Range(0, 100)] public float _Shop; 
    [Range(0, 100)] public float _Reward; 
}
