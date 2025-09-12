using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class Room : MonoBehaviour
{
    [Header("방 위치")] public Vector2Int Position;
    [Header("이동 가능한 방")] public List<Room> ConnectedRooms = new List<Room>();
    [Header("구역 ID")] public int ZoneID = -1;
    [Header("방 타입")] public RoomType Type;
    [Header("방 진입 여부")] public bool visite = false;

    RoomTouchEvent roomTouchEvent;

    private void Start()
    {
        roomTouchEvent = gameObject.AddComponent<RoomTouchEvent>();
        roomTouchEvent.Init(this);
    }

}

