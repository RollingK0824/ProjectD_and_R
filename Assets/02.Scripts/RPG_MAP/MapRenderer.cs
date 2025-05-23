using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapRenderer : MonoBehaviour
{
    [SerializeField] RectTransform _MapParent;
    [SerializeField] GameObject[] _RoomUis;

    /// <summary>
    /// 인게임 화면에 맵을 그려줌
    /// </summary>
    /// <param name="room">화면에 그려줄 맵</param>
    public void DrawMap(Room room)
    {
        GameObject roomUI = Instantiate(_RoomUis[Convert.ToInt32(room.Type)], _MapParent);
        
        roomUI.AddComponent<Room>();
        roomUI.GetComponent<Room>().Init(room.Type, room.XPos, room.YPos);
        roomUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(room.XPos * 100, room.YPos * 100);
        roomUI.name = room.Name;
    }
}

