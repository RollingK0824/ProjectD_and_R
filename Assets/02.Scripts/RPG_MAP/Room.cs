using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { Normal, Elite, Event, Shop, Reward, None }

public class Room : MonoBehaviour 
{
    [Header("방 위치")] public Vector2Int Position;
    [Header("이동 가능한 방")]public List<Room> ConnectedRooms = new List<Room>();
    [Header("방 타입")] public RoomType Type;
}

