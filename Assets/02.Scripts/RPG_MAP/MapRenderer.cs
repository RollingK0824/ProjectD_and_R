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
    /// �ΰ��� ȭ�鿡 ���� �׷���
    /// </summary>
    /// <param name="room">ȭ�鿡 �׷��� ��</param>
    public void DrawMap(Room room)
    {
        GameObject roomUI = Instantiate(_RoomUis[Convert.ToInt32(room.Type)], _MapParent);
        
        roomUI.AddComponent<Room>();
        roomUI.GetComponent<Room>().Init(room.Type, room.XPos, room.YPos);
        roomUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(room.XPos * 100, room.YPos * 100);
        roomUI.name = room.Name;
    }
}

