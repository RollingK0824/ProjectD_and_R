using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomTouchEvent : MonoBehaviour, ITouchble
{
    Room room;
    [Header("���� ���É���� Ȯ��")]public bool _isRoomSelect = false;

    //�׽�Ʈ��
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

            //��ɸ� ���� ����ȭ x
            originalColor = room.gameObject.GetComponent<Renderer>().material.color;
            room.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public void OnEmptyTouch()
    {
        Debug.Log("�����");
        ReturnSelectRoom();
    }

    public void OnOtherTouch()
    {
        Debug.Log("�ٸ�����");
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
