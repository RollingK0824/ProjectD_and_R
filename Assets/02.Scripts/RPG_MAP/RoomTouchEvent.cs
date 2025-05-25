using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomTouchEvent : MonoBehaviour, ITouchble
{
    Room room;
    public void OnTouch()
    {
        if(room!=null)
        {
            Debug.Log("선택 위치 " + room.YPos + "," + room.XPos);
        }
    }

    public void Init(Room R)
    {
        room = R;
    }
}
