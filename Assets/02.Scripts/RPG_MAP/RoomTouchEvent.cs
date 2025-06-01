using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomTouchEvent : MonoBehaviour, ITouchble
{
    Room room;
    [Header("방이 선택됬는지 확인")]public bool _isRoomSelect = false;

    //테스트용
    public Color originalColor;

    public void OnTouch()
    {
    }

    public void OnEmptyTouch()
    {
        Debug.Log("빈공간");
        ReturnSelectRoom();
    }

    public void OnOtherTouch()
    {
        Debug.Log("다른공간");
        ReturnSelectRoom();
    }

    public void ReturnSelectRoom()
    {
        _isRoomSelect = false;
    }

    public void Init(Room R)
    {
        room = R;
    }


}
