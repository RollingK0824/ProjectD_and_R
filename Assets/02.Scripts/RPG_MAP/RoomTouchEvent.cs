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
            _isRoomSelect = true;

            //기능만 구현 최적화 x
            originalColor = room.gameObject.GetComponent<Renderer>().material.color;
            room.gameObject.GetComponent<Renderer>().material.color = Color.black;

            for (int i = 0; i < room.Next.Count; i++)
            {
                room.Next[i].gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }

    public void OnEmptyTouch()
    {
        TestTouch();
    }

    public void OnOtherTouch()
    {
        TestTouch();
    }

    public void TestTouch()
    {
        _isRoomSelect = false;
        room.gameObject.GetComponent<Renderer>().material.color = originalColor;
        for (int i = 0; i < room.Next.Count; i++)
        {
            room.Next[i].gameObject.GetComponent<Renderer>().material.color = room._Color;
        }
    }

    public void Init(Room R)
    {
        room = R;
    }

    void UpdateRoomVisual(GameObject roomObj, RoomType type)
    {
        Renderer renderer = roomObj.GetComponent<Renderer>();
        if (renderer != null)
        {
            switch (type)
            {
                case RoomType.Normal:
                    renderer.material.color = Color.white;
                    break;
                case RoomType.Elite:
                    renderer.material.color = Color.red;
                    break;
                case RoomType.Event:
                    renderer.material.color = Color.yellow;
                    break;
                case RoomType.Shop:
                    renderer.material.color = Color.green;
                    break;
                case RoomType.Reward:
                    renderer.material.color = Color.cyan;
                    break;
                default:
                    renderer.material.color = Color.gray;
                    break;
            }
        }
    }
}
