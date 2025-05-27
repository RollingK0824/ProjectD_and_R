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
        if (room != null)
        {
            if(_isRoomSelect)
            {
                RpgManager.Instance.EnterStage(room);
                ReturnSelectRoom();
            }

            _isRoomSelect = true;

            //기능만 구현 최적화 x
            originalColor = room.gameObject.GetComponent<Renderer>().material.color;
            room.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
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
        room.gameObject.GetComponent<Renderer>().material.color = originalColor;
    }

    public void Init(Room R)
    {
        room = R;
    }


}
